using System;
using DifferentTypesOfSorts;

namespace DifferentTypesOfSorts
{
    public static class SortTester<T> where T : IComparable
    {
        public static void testSort(TestingFunc del, ref T Array)
        {
            TestingFunc func = Sort.bubbleSort;
            System.Console.WriteLine("");
        }
        public delegate void TestingFunc(ref T[] Array);
    }
}