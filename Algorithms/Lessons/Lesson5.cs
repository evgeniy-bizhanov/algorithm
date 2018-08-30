
// Бижанов Евгений

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace Algorithms
{
    [Description("Урок 5 - Динамическое программирование. Поиск возвратом")]
    public partial class Lesson5 : BaseLesson
    {
        [Description("Задача 1 - Реализовать перевод из десятичной в двоичную систему")]
        #region
        [Milestone("Введите любое десятичное число", MilestoneAttribute.Milestones.Input)]
        [Milestone("Двоичное представление числа {0} - {1}", MilestoneAttribute.Milestones.Output)]
        public object[] Task1(params object[] args)
        {
            // Входные данные
            int number = int.Parse(args[0].ToString());

            // Алгоритм
            Stack<int> stack = new Stack<int>();

            while (number != 0)
            {
                stack.Push(number % 2);
                number /= 2;
            }

            // Вывод

            string binary = "";
            while (stack.Count != 0)
            {
                binary += " " + stack.Pop();
            }

            return new object[] { args[0], binary.TrimStart() };
        }
        #endregion

        [Description("\b    [-] Задача 2 - Добавить в программу «реализация стека на основе\r\n\t\t   односвязного списка» проверку на выделение памяти")]
        #region
        public object[] Task2(params object[] args)
        {
            // Входные данные

            // Алгоритм
            throw new NotImplementedException();

            // Вывод
        }
        #endregion

        [Description("\b    [-] Задача 3 - Является ли скобочная последовательность правильной")]
        #region
        public object[] Task3(params object[] args)
        {
            // Входные данные

            // Алгоритм
            throw new NotImplementedException();

            // Вывод
        }
        #endregion

        [Description("\b    [-] Задача 4 - Создать функцию, копирующую односвязный список")]
        #region
        public object[] Task4(params object[] args)
        {
            // Входные данные

            // Алгоритм
            throw new NotImplementedException();

            // Вывод
        }
        #endregion

        [Description("\b    [-] Задача 5 - Перевод из инфиксной записи арифм. выражения в постфиксную")]
        #region
        public object[] Task5(params object[] args)
        {
            // Входные данные

            // Алгоритм
            throw new NotImplementedException();

            // Вывод
        }
        #endregion

        [Description("Задача 6 - Реализовать очередь")]
        #region
        //[Milestone("Задача реализована без теста, см. код", MilestoneAttribute.Milestones.Input)]
        public object[] Task6(params object[] args)
        {
            // Входные данные
            Lesson.Queue<int> queue = new Lesson.Queue<int>();

            // Алгоритм
            for (int i = 4; i <= 8; i++)
            {
                queue.Enqueue(i);
                Console.WriteLine($"В очередь добавлен элемент {i}");
                Thread.Sleep(300);
            }

            // Вывод
            while (queue.Length != 0)
            {
                var element = queue.Dequeue();
                Console.WriteLine($"Из очереди извлечен элемент {element}");
                Thread.Sleep(300);
            }

            return new object[] { };
        }
        #endregion
    }
}


namespace Algorithms.Lesson
{
    class Queue<T>
    {
        private T[] array = new T[] { };

        public void Enqueue(T element)
        {
            ResizeArray(array.Length + 1);
            array[array.Length - 1] = element;
        }

        public T Dequeue()
        {
            var element = array[0];
            ReorganizeArray();

            return element;
        }

        private void ReorganizeArray()
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }

            ResizeArray(array.Length - 1);
        }

        private void ResizeArray(int size)
        {
            Array.Resize(ref array, size);
        }

        public int Length
        {
            get { return array.Length; }
        }
    }
}