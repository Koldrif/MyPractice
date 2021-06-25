using System;


namespace DifferentTypesOfSorts
{
    class Program
    {
        static void Main(string[] args)
        {
            var numShit = 9;
            // Generate an array
            var ArrayOfInt1 = new int[numShit];
            var rand = new Random(((int)DateTime.Now.ToOADate()));
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < numShit; i++)
            {
                // Array.Resize(ref ArrayOfInt1, ArrayOfInt1.Length + 1);
                ArrayOfInt1[i] = rand.Next(999);
            }
            stopwatch.Stop();
            System.Console.WriteLine($"Млоча съела говно за: {stopwatch.Elapsed}");



            // //Sort the array
            // System.Console.Write("Массив до сортировки:\t");
            // foreach (var item in ArrayOfInt1)
            // {
            //     System.Console.Write(" [ {0} ]", item);
            // }
            // System.Console.WriteLine();


            Sort.shakerSort2(ref ArrayOfInt1);


            System.Console.Write("Массив после сортровки:\t");
            foreach (var item in ArrayOfInt1)
            {
                System.Console.Write(" [ {0} ]", item);
            }



        }
    }
}


