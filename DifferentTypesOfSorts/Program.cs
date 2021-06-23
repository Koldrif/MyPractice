using System;


namespace DifferentTypesOfSorts
{
    class Program
    {
        readonly static int[] ArrayOfInt = new int[] { 0, 3, 4, 2, 1, 5, 6, 8, 7, 9 };
        static void Main(string[] args)
        {
            // Generate an array
            int[] ArrayOfInt1 = new int[0];
            var rand = new Random(((int)DateTime.Now.ToOADate()));
            for (int i = 0; i < 99; i++)
            {
                Array.Resize(ref ArrayOfInt1, ArrayOfInt1.Length + 1);
                ArrayOfInt1[i] = rand.Next(999);
            }


            //Sort the array
            System.Console.Write("Массив до сортировки:\t");
            foreach (var item in ArrayOfInt1)
            {
                System.Console.Write(" [ {0} ]", item);
            }
            System.Console.WriteLine();
            Sort.bubbleSort(ref ArrayOfInt1);
            System.Console.Write("Массив после сортровки:\t");
            foreach (var item in ArrayOfInt1)
            {
                System.Console.Write(" [ {0} ]", item);
            }
        }


    }
}
