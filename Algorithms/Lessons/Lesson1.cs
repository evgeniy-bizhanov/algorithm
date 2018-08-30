
// Бижанов Евгений

using System;
using System.ComponentModel;

namespace Algorithms
{
    [Description("Урок 1 - Простые алгоритмы")]
    public class Lesson1 : BaseLesson
    {
        [Description("Задача 1 - Рассчет индекса массы тела")]
        [Milestone("Через пробел введите рост (см) и вес (кг) человека", MilestoneAttribute.Milestones.Input)]
        [Milestone("Индекс массы тела: {0:0.00}", MilestoneAttribute.Milestones.Output)]
        public object[] Task1(params object[] args)
        {
            // Входные данные
            double height = double.Parse(args[0].ToString()) / 100;
            double weight = double.Parse(args[1].ToString());

            // Алгоритм
            double index = weight / Math.Pow(height, 2);

            // Вывод
            return new object[] { index };
        }

        [Description("Задача 2 - Поиск максимального из четырех чисел")]
        [Milestone("Через пробел введите 4 числа", MilestoneAttribute.Milestones.Input)]
        [Milestone("Максимальное из введенных чисел - {0:0}, число выполненных операций - {1}", MilestoneAttribute.Milestones.Output)]
        public object[] Task2(params object[] args)
        {
            // Входные данные
            double number1 = double.Parse(args[0].ToString());
            double number2 = double.Parse(args[1].ToString());
            double number3 = double.Parse(args[2].ToString());
            double number4 = double.Parse(args[3].ToString());

            // Алгоритм
            int count = 1;
            double result = number1;

            count++;
            if (result < number2)
            {
                count++;
                result = number2;
            }

            count++;
            if (result < number3)
            {
                count++;
                result = number3;
            }

            count++;
            if (result < number4)
            {
                count++;
                result = number4;
            }

            // Вывод
            return new object[] { result, count };
        }

        [Description("Задача 3 - Обмен значениями двух чисел")]
        [Milestone("Через пробел введите 2 целочисленных числа\n\rДобавьте через пробел 1, если необходимо использовать дополнительную переменную", MilestoneAttribute.Milestones.Input)]
        [Milestone("Результат обмена значениями: [{0}, {1}] -> [{2}, {3}]", MilestoneAttribute.Milestones.Output)]
        public object[] Task3(params object[] args)
        {
            // Входные данные
            int number1 = int.Parse(args[0].ToString());
            int number2 = int.Parse(args[1].ToString());

            bool useVariable = false;
            if (args.Length > 2)
            {
                useVariable = int.Parse(args[2].ToString()) > 0;
            }

            var result = new object[4];
            result[0] = number1;
            result[1] = number2;

            // Алгоритм
            if (useVariable) // используем промежуточную переменную
            {
                var number = number1;
                number1 = number2;
                number2 = number;
            }
            else            // не используем переменную
            {
                number1 = number1 ^ number2;
                number2 = number1 ^ number2;
                number1 = number1 ^ number2;
            }

            // Вывод
            result[2] = number1;
            result[3] = number2;
            return result;
        }

        [Description("Задача 4 - Поиск корней квадратного уравнения")]
        [Milestone("Через пробел введите коэффициенты квадратного уравнения a, b, c", MilestoneAttribute.Milestones.Input)]
        [Milestone("Решение квадратного уравнения - {0}", MilestoneAttribute.Milestones.Output)]
        public object[] Task4(params object[] args)
        {
            // Входные данные
            double a = double.Parse(args[0].ToString());
            double b = double.Parse(args[1].ToString());
            double c = double.Parse(args[2].ToString());

            // Алгоритм
            double x1;
            double x2;

            string result;

            // Дискриминант
            double d = Math.Pow(b, 2) - 4 * a * c;

            // Нет корней
            if (d < 0)
            {
                result = "вещественных корней нет";
                return new object[] { result };
            }

            // Один корень
            if (d == 0)
            {
                x1 = -b / 2 * a;

                result = string.Format("найден один вещественный корень, X = {0:0.0}", x1);
                return new object[] { result };
            }

            // Два корня
            x1 = (-b + Math.Sqrt(d)) / 2 * a;
            x2 = (-b - Math.Sqrt(d)) / 2 * a;

            result = string.Format("найдены два вещественных корня, X1 = {0:0.0}; X2 = {1:0.0}", x1, x2);

            // Вывод
            return new object[] { result };
        }
    }
}
