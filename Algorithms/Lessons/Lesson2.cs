
// Бижанов Евгений

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithms
{
    [Description("Урок 2 - Асимптотическая сложность. Рекурсия")]
    public class Lesson2 : BaseLesson
    {
        [Description("Задача 1 - Перевести число из десятичной системы в двоичную")]
        #region
        [Milestone("Введите целое положительное число", MilestoneAttribute.Milestones.Input)]
        [Milestone("Двоичная форма представления числа '{0}':\n\r   {1}", MilestoneAttribute.Milestones.Output, WriteTimestampToConsole = true)]
        public object[] Task1(params object[] args)
        {
            // Входные данные
            int number = int.Parse(args[0].ToString());

            // Алгоритм
            string binary = Task1_GetBinaryRecursively(number);

            // Вывод
            return new object[] { number, binary.Trim() };
        }

        private string Task1_GetBinaryRecursively(int number)
        {
            string result = $"{number % 2}";
            int newNumber = number / 2;

            if (newNumber > 0)
            {
                result = $"{Task1_GetBinaryRecursively(newNumber)}{result}";
            }

            return result;
        }
        #endregion

        [Description("Задача 2 - Возведение числа 'a' в степень 'b'")]
        #region
        [Milestone("Введите число a:", MilestoneAttribute.Milestones.Input)]
        [Milestone("Введите число b:", MilestoneAttribute.Milestones.Input)]
        [Milestone("Выбирите один из вариантов:", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("    1. Без рекурсии", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("    2. Рекурсивно", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("    3. Используя свойство четности", MilestoneAttribute.Milestones.Input)]
        [Milestone("Результат: {0} ^ {1} = {2}", MilestoneAttribute.Milestones.Output)]
        public object[] Task2(params object[] args)
        {
            // Входные данные
            long a = long.Parse(args[0].ToString());
            long b = long.Parse(args[1].ToString());
            int option = int.Parse(args[2].ToString());

            if (option < 1 || option > 3)
            {
                throw new ArgumentOutOfRangeException("Вариант решения", $"Вариант под номером {option} не существует");
            }

            long result = 0;

            // Алгоритм
            switch (option)
            {
                case 1:
                    result = Task2_Pow(a, b);
                    break;
                case 2:
                    result = Task2_PowRecursively(a, b);
                    break;
                case 3:
                    result = Task2_PowRecursively_UsingParityOfNumber(a, b);
                    break;
            }

            // Вывод
            return new object[] { a, b, result };
        }

        // Возведение в степень
        private long Task2_Pow(long a, long b)
        {
            long result = b == 0 ? 1 : a;

            while (b > 1)
            {
                result *= a;
                b--;
            }

            return result;
        }

        // Возведение в степень рекурсивно
        private long Task2_PowRecursively(long a, long b)
        {
            long result = b == 0 ? 1 : a;

            if (b > 1)
            {
                result *= Task2_PowRecursively(a, --b);
            }

            return result;
        }

        // Возведение в степень рекурсивно, используя свойство четности
        private long Task2_PowRecursively_UsingParityOfNumber(long a, long b)
        {
            long result = 1;

            if (b > 0)
            {
                if (b % 2 > 0)
                {
                    result *= a;
                }

                a *= a;

                result *= Task2_PowRecursively_UsingParityOfNumber(a, b / 2);
            }

            return result;
        }
        #endregion

        [Description("Задача 3 - Калькулятор")]
        #region
        [Milestone("У исполнителя две команды:", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("    1. Прибавь 1", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("    2. Умножь на 2", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("\n\rСколько существует программ, которые преобразуют число 3 в число 20?", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("Выберите один из вариантов решения:", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("    1. С использованием массива", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("    2. С использованием рекурсии", MilestoneAttribute.Milestones.Input)]
        [Milestone("\n\rКол-во необходимых программ, для преобразования числа a={0} в число b={1}: {2}", MilestoneAttribute.Milestones.Output)]
        public object[] Task3(params object[] args)
        {
            // Входные данные
            int a = 3;
            int b = 20;
            int option = int.Parse(args[0].ToString());

            if (option < 1 || option > 2)
            {
                throw new ArgumentOutOfRangeException("Вариант решения", $"Вариант под номером {option} не существует");
            }

            int result = 0;

            // Алгоритм
            switch (option)
            {
                case 1:
                    result = Task3_Calculate(a, b);
                    break;
                case 2:
                    result = Task3_CalculateRecursively(a, b);
                    break;
            }

            // Вывод
            return new object[] { a, b, result };
        }

        // С использованием массива
        private int Task3_Calculate(int a, int b)
        {
            List<int> array = new List<int>();
            array.Add(1);

            for (int i = a + 1; i <= b; i++)
            {
                if (i % 2 != 0)
                {
                    array.Add(array[i - a - 1]);
                }
                else
                {
                    array.Add(array[(i - a) / 2] + array[i - a - 1]);
                }
            }

            return array.Last();
        }

        // С использованием рекурсии
        private int Task3_CalculateRecursively(int a, int b)
        {
            throw new NotImplementedException("Поиск программ для преобразования числа с использованием рекурсии не реализован");
        }
        #endregion
    }
}
