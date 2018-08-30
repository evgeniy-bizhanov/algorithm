
// Бижанов Евгений

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Algorithms
{
    [Description("Урок 3 - Поиск в массиве. Простые сортировки")]
    public class Lesson3 : BaseLesson
    {
        [Description("Задача 1 - Пузырьковая сортировка")]
        #region
        [Milestone("Введите целое положительное число (размер массива)", MilestoneAttribute.Milestones.Input)]
        [Milestone("Массив из {0} чисел отсортирован" +
            "\n\r   Кол-во перестановок при сортировке пузырьком: {1}" +
            "\n\r   Кол-во перестановок при улучшенной сортировке пузырьком: {2}" +
            "\r\n   Затрачено времени на сортировку (мс):\n\r    {3}" +
            "\r\n   Кол-во операций сравнения:\n\r    {4}", MilestoneAttribute.Milestones.Output)]
        public object[] Task1(params object[] args)
        {
            // Входные данные
            int count = int.Parse(args[0].ToString());
            int[] numbers = new int[count];

            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                numbers[i] = random.Next(1, count);
            }

            int[] numbers1 = new int[count];
            int[] numbers2 = new int[count];

            // можно было передавать не по ссылке, но
            // сделал уже что бы возвращался счетчик, решил оставить так
            numbers.CopyTo(numbers1, 0); // для первого алгоритма
            numbers.CopyTo(numbers2, 0); // для второго алгоритма

            // Алгоритм
            long counter = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            long count1 = Task1_BubbleSort(ref numbers1, out counter);
            stopwatch.Stop();

            long count2 = 0;// Task1_BubbleSortImproved(ref numbers2);

            // Вывод
            return new object[] { count, count1, count2, stopwatch.ElapsedMilliseconds, counter };
        }

        // Сортировка пузырьком
        private long Task1_BubbleSort(ref int[] numbers, out long counter)
        {
            long count = 0;
            counter = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    counter++;
                    if (numbers[j] > numbers[j + 1])
                    {
                        count++;
                        numbers[j] = numbers[j] ^ numbers[j + 1];
                        numbers[j + 1] = numbers[j] ^ numbers[j + 1];
                        numbers[j] = numbers[j] ^ numbers[j + 1];
                    }
                }
            }

            return count;
        }

        // Совершенно случайно улучшил :)
        // Сначала подумал что одно и то же по большому счету, кол-во
        // итераций приблизительно равно.
        // 
        // Однако нет, кол-во перестановки элементов при больших размерах
        // массива оказалась чуть ли не в 2 раза меньше
        private long Task1_BubbleSortImproved(ref int[] numbers)
        {
            long count = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (numbers[j] > numbers[i])
                    {
                        count++;
                        numbers[i] = numbers[i] ^ numbers[j];
                        numbers[j] = numbers[i] ^ numbers[j];
                        numbers[i] = numbers[i] ^ numbers[j];
                    }
                }
            }

            return count;
        }
        #endregion

        [Description("Задача 2 - Шейкерная сортировка")]
        #region
        [Milestone("Введите целое положительное число (размер массива)", MilestoneAttribute.Milestones.Input)]
        [Milestone("Массив из {0} чисел отсортирован" +
            "\n\r   Кол-во итераций: {1}" +
            "\r\n   Затрачено времени на сортировку (мс):\n\r    {2}" +
            "\r\n   Кол-во операций сравнения:\n\r    {3}", MilestoneAttribute.Milestones.Output)]
        public object[] Task2(params object[] args)
        {
            // Входные данные
            int count = int.Parse(args[0].ToString());
            int[] numbers = new int[count];

            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                numbers[i] = random.Next(1, count);
            }

            // Алгоритм
            long counter = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            var metric = Task2_ShakerSort(ref numbers, out counter);
            stopwatch.Stop();

            // Вывод
            return new object[] { count, metric, stopwatch.ElapsedMilliseconds, counter };
        }

        // Шейкерная сортировка
        private long Task2_ShakerSort(ref int[] numbers, out long counter)
        {
            long count = 0;
            counter = 0;

            var start = 0;
            int end = numbers.Length - 1;

            do
            {
                count++;
                for (int j = start; j < end; j++)
                {
                    count++;
                    counter++;
                    if (numbers[j] > numbers[j + 1])
                    {
                        numbers[j] = numbers[j] ^ numbers[j + 1];
                        numbers[j + 1] = numbers[j] ^ numbers[j + 1];
                        numbers[j] = numbers[j] ^ numbers[j + 1];
                    }
                }

                for (int j = end; j > start; j--)
                {
                    count++;
                    counter++;
                    if (numbers[j] < numbers[j - 1])
                    {
                        numbers[j] = numbers[j] ^ numbers[j - 1];
                        numbers[j - 1] = numbers[j] ^ numbers[j - 1];
                        numbers[j] = numbers[j] ^ numbers[j - 1];
                    }
                }

                start++;
                end--;

                counter++;
            } while (start <= end);

            return count;
        }
        #endregion

        [Description("Задача 3 - Бинарный алгоритм поиска")]
        #region
        [Milestone("В качестве упорядоченного массива будет использован", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("массив чисел Фибоначчи", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("---------------------------------------------------", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("Введите целое положительное число, которое необходимо найти", MilestoneAttribute.Milestones.Input)]
        [Milestone("{0}", MilestoneAttribute.Milestones.Output)]
        public object[] Task3(params object[] args)
        {
            // Входные данные
            long searchNumber = long.Parse(args[0].ToString());
            long[] numbers = GetFibonacci(50);

            // Алгоритм
            var index = Task3_BinarySearch(numbers, searchNumber);

            // Вывод
            var result = "Упс. Похоже, что такого числа Фибоначчи не существует";
            if (index.HasValue)
            {
                result = $"Индекс искомого числа Фибоначчи - {index.Value}";
            }
            
            return new object[] { result };
        }

        // Бинарный поиск
        private int? Task3_BinarySearch(long[] numbers, long searchNumber)
        {
            var start = 1;
            var end = numbers.Length;
            var middle = 0;

            do
            {
                middle = start + (end - start) / 2;

                if (numbers[middle] < searchNumber)
                {
                    start = middle + 1;
                }
                else
                {
                    end = middle - 1;
                }
            } while (start <= end && numbers[middle] != searchNumber);

            if (numbers[middle] == searchNumber)
            {
                return middle;
            }

            return null;
        }

        private long[] GetFibonacci(int count)
        {
            long[] array = new long[count];

            array[0] = 1;
            array[1] = 1;

            for (int i = 2; i < count; i++)
            {
                array[i] = array[i - 1] + array[i - 2];
            }

            return array;
        }

        #endregion
    }
}
