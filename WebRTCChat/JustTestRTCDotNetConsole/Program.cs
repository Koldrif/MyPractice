using System;
using System.Threading.Tasks;
using Microsoft.MixedReality.WebRTC;
using NamedPipeSignaler;
using WellcomeToTheCumZone;

namespace JustTestRTCDotNetConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Check args for keywords
            bool needVideo = Array.Exists(args, (arg) =>
            {
                return (arg == "-v") || (arg == "--video");
            });
            bool needAudio = Array.Exists(args, arg => arg == "-a" || arg == "--audio");


            // Adding local media tracks
            // Some variables
            AudioTrackSource microphoneSource = null;
            VideoTrackSource webcamSource = null;
            Transceiver audioTransceiver = null;
            Transceiver videoTransceiver = null;
            LocalAudioTrack localAudioTrack = null;
            LocalVideoTrack localVideoTrack = null;
            try
            {
                // Creating and binding audio and vidio sources
                // Create Video track source
                webcamSource = await DeviceVideoTrackSource.CreateAsync();
                var VideoTrackConfig = new LocalVideoTrackInitConfig { trackName = "webcam_track" };
                localVideoTrack = LocalVideoTrack.CreateFromSource(webcamSource, VideoTrackConfig);

                // Create Audio track source
                microphoneSource = await DeviceAudioTrackSource.CreateAsync();
                var audioTrackConfig = new LocalAudioTrackInitConfig { trackName = "microphone_track" };
                localAudioTrack = LocalAudioTrack.CreateFromSource(microphoneSource, audioTrackConfig);

                // Getting Video sources
                printLog();
                System.Console.WriteLine("*******Getting Vidio sources*******");
                var deviceList = await DeviceVideoTrackSource.GetCaptureDevicesAsync();

                System.Console.WriteLine($"Found {deviceList.Count}:");
                {
                    int i = 0;
                    foreach (var device in deviceList)
                    {
                        System.Console.WriteLine($"\t{i + 1}. device {device.name} has id \"{device.id}\"\n");
                        i++;
                    }
                }
                // End of getting Vidio sources

                // Creating a peer connection
                using var pc = new PeerConnection() { Name = "Test pc" };

                var config = new PeerConnectionConfiguration
                {
                    IceServers = new System.Collections.Generic.List<IceServer>
                    {
                        new IceServer{ Urls = {"stun:stun.l.google.com:19302"}}
                    }
                };
                await pc.InitializeAsync();
                System.Console.WriteLine("Connection initialized");
                printLog();
                System.Console.WriteLine($"pc.Name = {pc.Name}");



                //Binding audio and video to the pc
                videoTransceiver = pc.AddTransceiver(MediaKind.Video);
                videoTransceiver.LocalVideoTrack = localVideoTrack;
                videoTransceiver.DesiredDirection = Transceiver.Direction.SendReceive;

                audioTransceiver = pc.AddTransceiver(MediaKind.Audio);
                audioTransceiver.LocalAudioTrack = localAudioTrack;
                audioTransceiver.DesiredDirection = Transceiver.Direction.SendReceive;

                // Создание сигналлера

                var signaller = new NamedPipeSignaler.NamedPipeSignaler(pc, "testpipe");
                signaller.SdpMessageReceived += async (SdpMessage message) =>
                {
                    await pc.SetRemoteDescriptionAsync(message);
                    if (message.Type == SdpMessageType.Offer)
                    {
                        pc.CreateAnswer();
                    }
                };

                signaller.IceCandidateReceived += (IceCandidate candidate) =>
                {
                    pc.AddIceCandidate(candidate);
                };
                await signaller.StartAsync();


                // Record video from local webcam, and send to remote peer
                if (needVideo)
                {
                    Console.WriteLine("Opening local webcam...");
                    localVideoTrack = LocalVideoTrack.CreateFromSource(webcamSource, VideoTrackConfig);
                    videoTransceiver = pc.AddTransceiver(MediaKind.Video);
                    videoTransceiver.DesiredDirection = Transceiver.Direction.SendReceive;
                    videoTransceiver.LocalVideoTrack = localVideoTrack;
                }

                // Record audio from local microphone, and send to remote peer
                if (needAudio)
                {
                    Console.WriteLine("Opening local microphone...");
                    localAudioTrack = LocalAudioTrack.CreateFromSource(microphoneSource, audioTrackConfig);
                    audioTransceiver = pc.AddTransceiver(MediaKind.Audio);
                    audioTransceiver.DesiredDirection = Transceiver.Direction.SendReceive;
                    audioTransceiver.LocalAudioTrack = localAudioTrack;
                }

                pc.Connected += () => System.Console.WriteLine("Peer connection: Connected");
                pc.IceStateChanged += (IceConnectionState newICEState) => System.Console.WriteLine($"ICE state = {newICEState}");

                int videoFrames = 0;
                pc.VideoTrackAdded += (RemoteVideoTrack track) =>
                {
                    track.I420AVideoFrameReady += (I420AVideoFrame frame) =>
                    {
                        ++videoFrames;
                        if (videoFrames % 60 == 0)
                        {
                            System.Console.WriteLine($"Recieved video frames: {videoFrames}");
                        }
                    };

                };

                if (signaller.IsClient)
                {
                    System.Console.WriteLine("Connecting to remote peer");
                    pc.CreateOffer();
                }
                else
                {
                    System.Console.WriteLine("Waiting for other remote peer");
                }
                Console.WriteLine("Press a key to terminate the application...");
                Console.ReadKey(true);
                signaller.Stop();
                Console.WriteLine("Program termined.");


            }
            catch (System.Exception e)
            {

                System.Console.WriteLine(e.Message);
            }

            localAudioTrack?.Dispose();
            localVideoTrack?.Dispose();
            microphoneSource?.Dispose();
            webcamSource?.Dispose();
        }

        static void printLog()
        {
            System.Console.WriteLine("*******Log*******");
        }
    }
}
