using System;

namespace DifferentTypesOfSorts
{
    public static class Sort
    {
        private static void swap<T>(ref T[] items, int IndLeft, int IndRight)
        {
            if (IndLeft != IndRight)
            {
                var temp = items[IndLeft];
                items[IndLeft] = items[IndRight];
                items[IndRight] = temp;
            }
        }
        private static void swap<T>(ref T IndLeft, ref T IndRight) where T : IComparable
        {
            if (IndLeft.CompareTo(IndRight) != 0)
            {
                var temp = IndLeft;
                IndLeft = IndRight;
                IndRight = temp;
            }
        }

        public static void bubbleSort<T>(ref T[] inputArr) where T : IComparable
        {
            System.Console.WriteLine("\nStarted bubble sort...");
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
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
            stopwatch.Stop();
            System.Console.WriteLine();
            Console.WriteLine($"Код выполнялся: {stopwatch.Elapsed}");
            return;
        }

        // Более медленный вариант и сложный имхо
        public static void bubbleSort2<T>(ref T[] inputArr) where T : IComparable
        {
            System.Console.WriteLine("\nStarted another bubble sort...");
            bool flag = false;
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            do
            {
                flag = false;
                for (int i = 0; i + 1 < inputArr.Length - i; i++)
                {
                    if (inputArr[i].CompareTo(inputArr[i + 1]) > 0)
                    {
                        swap(ref inputArr, i, i + 1);
                        flag = true;
                    }
                }
            } while (flag);

            stopwatch.Stop();
            Console.WriteLine($"Код выполнялся: {stopwatch.Elapsed}\n\n");
        }

        public static void shakerSort<T>(ref T[] inputArr) where T : IComparable
        {
            bool flag;
            var timer = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < inputArr.Length / 2; i++)
            {
                flag = false;
                // Перенос нибольщего в конец массива
                for (int j = i; j < inputArr.Length - 1 - i; j++)
                {
                    if (inputArr[j].CompareTo(inputArr[j + 1]) > 0)
                    {
                        swap(ref inputArr[j], ref inputArr[j + 1]);
                        flag = true;
                    }
                }
                //Перенос наименшьего в начало массива
                for (int j = inputArr.Length - 2 - i; j - 1 > i; j--)
                {
                    if (inputArr[j].CompareTo(inputArr[j - 1]) < 0)
                    {
                        swap(ref inputArr[j - 1], ref inputArr[j]);
                        flag = true;
                    }
                }
                if (!flag)
                {
                    break;
                }
            }
            timer.Stop();
            System.Console.WriteLine("Код отработал: {0}", timer.Elapsed);
            return;
        }

        public static void shakerSort2<T>(ref T[] inputArr) where T : IComparable
        {
            bool flag;
            var timer = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < inputArr.Length / 2; i++)
            {
                flag = false;
                // Перенос нибольщего в конец массива
                for (int j = i, k = inputArr.Length - 1 - i; (j < inputArr.Length - 1 - i || k - 1 > i); j++, k--)
                {
                    if (inputArr[j].CompareTo(inputArr[j + 1]) > 0)
                    {
                        swap(ref inputArr[j], ref inputArr[j + 1]);
                        flag = true;
                    }
                    if (inputArr[k].CompareTo(inputArr[k - 1]) < 0)
                    {
                        swap(ref inputArr[k - 1], ref inputArr[k]);
                        flag = true;
                    }

                }
                if (!flag)
                {
                    break;
                }
            }
            timer.Stop();
            System.Console.WriteLine("Код отработал: {0}", timer.Elapsed);
            return;
        }
    }
}