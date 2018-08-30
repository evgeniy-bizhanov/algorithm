
// Бижанов Евгений

#region Таблица сравнений алгоритмов сортировки
//
//========================================================================
// Процессор         | Intel Core i7-6700HQ 2.60GHz (1 ядро), виртуальная машина
//========================================================================
//::::::: 100 :::::::|        Время, мс       |      Кол-во сравнений
//========================================================================
// QuickSort/L8,T2   |            0           |            773
//------------------------------------------------------------------------
// Merge Sort/L8,T3  |            0           |            2227
//------------------------------------------------------------------------
// Bubble Sort/L3,T1 |            0           |            9900
//------------------------------------------------------------------------
// Shake Sort/L3,T2  |            0           |            5050
//========================================================================
//::::: 10 000 ::::::|        Время, мс       |      Кол-во сравнений
//========================================================================
// QuickSort/L8,T2   |            1           |           124039
//------------------------------------------------------------------------
// Merge Sort/L8,T3  |            2           |           424298
//------------------------------------------------------------------------
// Bubble Sort/L3,T1 |           575          |          99990000
//------------------------------------------------------------------------
// Shake Sort/L3,T2  |           278          |          50005000
//========================================================================
//:::: 1 000 000 ::::|        Время, мс       |      Кол-во сравнений
//========================================================================
// QuickSort/L8,T2   |           166          |          16865955
//------------------------------------------------------------------------
// Merge Sort/L8,T3  |           313          |          62301094
//------------------------------------------------------------------------
// Bubble Sort/L3,T1 |         5291195	      |        999999000000
//------------------------------------------------------------------------
// Shake Sort/L3,T2  |         2990362	      |        500000500000
//========================================================================
//
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
    [Description("Урок 8 - Сложные сортировки")]
    public partial class Lesson8 : BaseLesson
    {
        //
        // Матрица смежности:
        //
        string[] matrixRepresentation = new string[]
        {
            "0 4 0 0",
            "0 0 3 6",
            "0 0 2 0",
            "5 6 0 0"
        };

        [Description("Задача 1 - Реализовать сортировку подсчетом")]
        #region
        [Milestone("Введите целое положительное число - кол-во элементов в массиве:", MilestoneAttribute.Milestones.Input)]
        [Milestone("Исходный массив:\n\r    {0}"+
            "\r\nОтсортированный массив:\n\r    {1}"+
            "\r\nЗатрачено времени на сортировку (мс):\n\r    {2}", MilestoneAttribute.Milestones.Output)]
        public object[] Task1(params object[] args)
        {
            // Входные данные
            int count = int.Parse(args[0].ToString());
            int[] array = PrepareArray(count);

            string input = ReduceArray(array);

            // Алгоритм
            Stopwatch stopwatch = Stopwatch.StartNew();
            Task1_CountingSort(ref array);
            stopwatch.Stop();

            // Вывод
            var output = ReduceArray(array, 24);

            return new object[] { input, output, stopwatch.ElapsedMilliseconds };
        }

        private void Task1_CountingSort(ref int[] array)
        {
            int[] counts = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                counts[array[i]]++;
            }

            int index = 0;

            for (int j = 0; j < array.Length; j++)
            {
                for (int i = 0; i < counts[j]; i++)
                {
                    array[index++] = j;
                }
            }
        }
        #endregion

        [Description("Задача 2 - Реализовать быструю сортировку")]
        #region
        [Milestone("Введите целое положительное число - кол-во элементов в массиве:", MilestoneAttribute.Milestones.Input)]
        [Milestone("Исходный массив:\n\r    {0}" +
            "\r\nОтсортированный массив:\n\r    {1}" +
            "\r\nЗатрачено времени на сортировку (мс):\n\r    {2}" +
            "\r\nКол-во операций сравнения:\n\r    {3}", MilestoneAttribute.Milestones.Output)]
        public object[] Task2(params object[] args)
        {
            // Входные данные
            int count = int.Parse(args[0].ToString());
            int[] array = PrepareArray(count);

            string input = ReduceArray(array);

            // Алгоритм
            Stopwatch stopwatch = Stopwatch.StartNew();
            var comparisons = Task2_QuickSort(ref array, 0, array.Length - 1);
            stopwatch.Stop();

            // Вывод
            var output = ReduceArray(array, 24);

            return new object[] { input, output, stopwatch.ElapsedMilliseconds, comparisons };
        }

        private long Task2_QuickSort(ref int[] array, int start, int end)
        {
            int first = start, last = end;
            int x = array[(first + last) / 2];

            long counter = 0;

            do
            {
                while (array[first] < x)
                {
                    first++;
                }

                while (array[last] > x)
                {
                    last--;
                }

                counter++;
                if (first > last)
                {
                    continue;
                }

                counter++;
                if (array[first] > array[last])
                {
                    array[first] = array[first] ^ array[last];
                    array[last] = array[first] ^ array[last];
                    array[first] = array[first] ^ array[last];
                }

                first++;
                last--;

                counter++;
            } while (first <= last);

            counter++;
            if (first < end)
            {
                counter += Task2_QuickSort(ref array, first, end);
            }

            counter++;
            if (start < last)
            {
                counter += Task2_QuickSort(ref array, start, last);
            }

            return counter;
        }
        #endregion

        [Description("Задача 3 - Реализовать сортировку слиянием")]
        #region
        [Milestone("Введите целое положительное число - кол-во элементов в массиве:", MilestoneAttribute.Milestones.Input)]
        [Milestone("Исходный массив:\n\r    {0}" +
            "\r\nОтсортированный массив:\n\r    {1}" +
            "\r\nЗатрачено времени на сортировку (мс):\n\r    {2}" +
            "\r\nКол-во операций сравнения:\n\r    {3}", MilestoneAttribute.Milestones.Output)]
        public object[] Task3(params object[] args)
        {
            // Входные данные
            int count = int.Parse(args[0].ToString());
            int[] array = PrepareArray(count);

            string input = ReduceArray(array);

            // Алгоритм
            Stopwatch stopwatch = Stopwatch.StartNew();
            var comparisons = Task3_MergeSort(ref array, 0, array.Length - 1);
            stopwatch.Stop();

            // Вывод
            var output = ReduceArray(array, 24);

            return new object[] { input, output, stopwatch.ElapsedMilliseconds, comparisons };
        }

        private long Task3_MergeSort(ref int[] array, int left, int right)
        {
            long counter = 0;

            counter++;
            if (left > right)
            {
                return counter;
            }

            counter++;
            if ((right - left) > 1)
            {
                int middle = (left + right) / 2;
                counter += Task3_MergeSort(ref array, left, middle);
                counter += Task3_MergeSort(ref array, middle, right);
                counter += Merge(ref array, left, middle, right);
                return counter;
            }

            counter++;
            if (array[right] < array[left])
            {
                array[left] = array[left] ^ array[right];
                array[right] = array[left] ^ array[right];
                array[left] = array[left] ^ array[right];
            }

            return counter;
        }

        private long Merge(ref int[] array, int left, int mid, int right)
        {
            long counter = 0;

            int iLeft = 0;
            int iRight = 0;
            int[] result = new int[right - left];

            while ((left + iLeft) < mid && (mid + iRight) < right)
            {
                counter++;
                counter++;
                counter++;
                if (array[left + iLeft] < array[mid + iRight])
                {
                    result[iLeft + iRight] = array[left + iLeft];
                    iLeft++;
                }
                else
                {
                    result[iLeft + iRight] = array[mid + iRight];
                    iRight++;
                }
            }

            while ((left + iLeft) < mid)
            {
                counter++;
                result[iLeft + iRight] = array[left + iLeft];
                iLeft++;
            }

            while ((mid + iRight) < right)
            {
                counter++;
                result[iLeft + iRight] = array[mid + iRight];
                iRight++;
            }

            for (int i = 0; i < iLeft + iRight; i++)
            {
                array[left + i] = result[i];
            }

            return counter;
        }
        #endregion

        [Description("Задача 4 - Реализовать алгоритм сортировки со списком")]
        #region
        [Milestone("Введите целое положительное число - кол-во элементов в массиве:", MilestoneAttribute.Milestones.Input)]
        [Milestone("Исходный массив:\n\r    {0}\r\nОтсортированный массив:\n\r    {1}", MilestoneAttribute.Milestones.Output)]
        public object[] Task4(params object[] args)
        {
            // Входные данные
            int count = int.Parse(args[0].ToString());
            int[] array = PrepareArray(count);

            string input = ReduceArray(array);

            // Алгоритм
            Task4_ListSort(ref array);

            // Вывод
            var output = ReduceArray(array, 24);

            return new object[] { input, output };
        }

        private void Task4_ListSort(ref int[] array)
        {
            int size = array.Max() + 1;
            List<int>[] aux = new List<int>[size];

            for (int i = 0; i < array.Length; i++)
            {
                aux[array[i]] = aux[array[i]] ?? new List<int>();
                aux[array[i]].Add(array[i]);
            }

            int k = 0;
            foreach (var numbers in aux)
            {
                if (numbers == null) continue;

                foreach (var number in numbers)
                {
                    array[k++] = number;
                }
            }
        }
        #endregion
    }

    // Вспомогательные
    partial class Lesson8
    {
        private static int[] PrepareArray(int count)
        {
            int[] array = new int[count];

            var rnd = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(0, count - 1);
            }

            return array;
        }

        private static string ReduceArray(int[] array, int reduceDepth = 0)
        {
            var output = "";

            var size = array.Length;

            if (reduceDepth == 0)
            {
                reduceDepth = array.Length;
                if (size > 20)
                {
                    reduceDepth = 18;
                }
                if (size > 100)
                {
                    reduceDepth = 16;
                }
                if (size > 1000)
                {
                    reduceDepth = 12;
                }
                if (size > 100000)
                {
                    reduceDepth = 10;
                }
            }
            else
            {
                reduceDepth = size < reduceDepth ? size : reduceDepth;
            }

            for (int i = 0; i < reduceDepth; i++)
            {
                output += array[i] + " ";
            }

            if (size > 20)
            {
                output += "...";
            }

            return output;
        }
    }
}
