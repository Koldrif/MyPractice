using System;


namespace DifferentTypesOfSorts
{
    class Program
    {
        static void Main(string[] args)
        {
            var ArrayOfInt = new int[] { 0, 3, 4, 2, 1, 5, 6, 8, 7, 9 };
            System.Console.Write("Array before sort:\t");
            foreach (var item in ArrayOfInt)
            {
                System.Console.Write($"[ {item} ] ");
            }
            Sort.bubbleSort(ref ArrayOfInt);
            System.Console.Write("\nArray after sort:\t");
            foreach (var item in ArrayOfInt)
            {
                System.Console.Write($"[ {item} ] ");
            }
        }


    }
}
