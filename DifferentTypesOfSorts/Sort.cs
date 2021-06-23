using System;

namespace DifferentTypesOfSorts
{
    public static class Sort
    {
        private static void swap<T>(ref T[] items, int IndLeft, int IndRight) where T : IComparable
        {
            if (IndLeft != IndRight)
            {
                var temp = items[IndLeft];
                items[IndLeft] = items[IndRight];
                items[IndRight] = temp;
            }
        }
        public static void bubbleSort<T>(ref T[] inputArr) where T : IComparable
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            // Some code here...
            stopwatch.Stop();
            for (int i = 0; i < inputArr.Length; i++)
            {
                for (int j = i + 1; j < inputArr.Length; j++)
                {
                    if (inputArr[i].CompareTo(inputArr[j]) > 0)
                    {
                        swap(ref inputArr, i, j);
                    }
                }
            }
            System.Console.WriteLine();
            Console.WriteLine($"Код выполнялся: {stopwatch.Elapsed}");
            return;
        }
    }
}