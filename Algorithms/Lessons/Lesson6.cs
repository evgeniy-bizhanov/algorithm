
// Бижанов Евгений

using System;
using System.ComponentModel;
using System.Text;

namespace Algorithms
{
    [Description("Урок 6 - Деревья")]
    public partial class Lesson6 : BaseLesson
    {
        [Description("Задача 1 - Реализовать простейшую хеш-функцию. На вход функции подается строка, на выходе сумма кодов символов")]
        #region
        [Milestone("Введите текст произвольной длины", MilestoneAttribute.Milestones.Input)]
        [Milestone("Входная строка '{0}', имеет хэш - {1}", MilestoneAttribute.Milestones.Output)]
        public object[] Task1(params object[] args)
        {
            // Входные данные
            string valueForHash = "";
            foreach (var arg in args)
            {
                valueForHash += $"{arg} ";
            }
            valueForHash = valueForHash.Trim();

            // Алгоритм
            long hash = Task1_GetHash(valueForHash);

            // Вывод
            return new object[] { valueForHash, hash };
        }

        private long Task1_GetHash(string value)
        {
            StringBuilder sb = new StringBuilder(value);
            long hash = 0;

            for (int i = 0; i < sb.Length; i++)
            {
                hash += sb[i];
            }

            return hash;
        }
        #endregion

        int[] numbers = new int[] { 52, 36, 8, 12, 97, 65, 48, 32, 71, 14 };

        [Description("Задача 2 - Переписать программу, реализующую двоичное дерево поиска")]
        #region
        [Milestone("-------------------------------------------------------------------------", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("Для решения задач будет использован следующий числовой набор:", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("    52, 36, 8, 12, 97, 65, 48, 32, 71, 14", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("-------------------------------------------------------------------------", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("Выберите один из вариантов (введите 1 или 2):", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("    1. Выполнить обход дерева (через пробел укажите способ обхода)", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("        1. КЛП", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("        2. ЛКП", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("        3. ЛПК", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("    2. Выполнить поиск в двоичном дереве поиска (через пробел укажите одно из чисел объявленных выше)", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("-------------------------------------------------------------------------", MilestoneAttribute.Milestones.Input)]
        [Milestone("{0}", MilestoneAttribute.Milestones.Output)]
        public object[] Task2(params object[] args)
        {
            // Входные данные
            string result;
            int subtask = GetSubtask(args, out result);
            int traverseWay, searchNumber;
            traverseWay = searchNumber = GetSecondNumber(args, subtask);

            // Алгоритм
            //
            // Построение дерева
            //
            Node<int> tree = null;

            foreach (var number in numbers)
            {
                Task2_InsertNode(ref tree, number);
            }

            //
            // Обход дерева
            //
            if (subtask == 1)
            {
                switch (traverseWay)
                {
                    case 1:
                        Task2_GoThroughBinaryTreeRecursively1(tree);
                        break;
                    case 2:
                        Task2_GoThroughBinaryTreeRecursively2(tree);
                        break;
                    case 3:
                        Task2_GoThroughBinaryTreeRecursively3(tree);
                        break;
                }
            }
            else if (subtask == 2)
            {
                if(!Task2_SearchBinaryTree(tree, searchNumber))
                {
                    result = $"Узел с числом {searchNumber} не обнаружен";
                }
            }

            // Вывод
            return new object[] { result };
        }

        void Task2_InsertNode(ref Node<int> root, int value)
        {
            if (root == null)
            {
                root = new Node<int>(value, null);
                return;
            }

            Node<int> node = root;

            while (true)
            {
                if (value > node.Data)
                {
                    if (node.Right != null)
                    {
                        node = node.Right;
                        continue;
                    }
                    else
                    {
                        node.Right = new Node<int>(value, node);
                        return;
                    }
                }
                else if (value < node.Data)
                {
                    if (node.Left != null)
                    {
                        node = node.Left;
                        continue;
                    }
                    else
                    {
                        node.Left = new Node<int>(value, node);
                        return;
                    }
                }
            }
        }

        // КЛП
        void Task2_GoThroughBinaryTreeRecursively1<T>(Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            node.Check();

            Task2_GoThroughBinaryTreeRecursively1(node.Left);
            Task2_GoThroughBinaryTreeRecursively1(node.Right);
        }

        // ЛКП
        void Task2_GoThroughBinaryTreeRecursively2<T>(Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            Task2_GoThroughBinaryTreeRecursively2(node.Left);
            node.Check();
            Task2_GoThroughBinaryTreeRecursively2(node.Right);
        }

        // ЛПК
        void Task2_GoThroughBinaryTreeRecursively3<T>(Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            Task2_GoThroughBinaryTreeRecursively3(node.Left);
            Task2_GoThroughBinaryTreeRecursively3(node.Right);
            node.Check();
        }

        bool Task2_SearchBinaryTree(Node<int> node, int number)
        {
            if (node == null)
            {
                return false;
            }

            if (node.Data == number)
            {
                Console.WriteLine($"Найден узел с числом {number}:\r\n{node}");
                return true;
            }

            else if (node.Data > number)
            {
                return Task2_SearchBinaryTree(node.Left, number);
            }

            else if (node.Data < number)
            {
                return Task2_SearchBinaryTree(node.Right, number);
            }

            return false;
        }
        #endregion

        [Description("Задача 3 - Разработать базу данных студентов")]
        #region
        [Milestone("{0}", MilestoneAttribute.Milestones.Output)]
        public object[] Task3(params object[] args)
        {
            // Входные данные

            // Алгоритм

            throw new NotImplementedException();

            // Вывод

            //return new object[] {  };
        }
        #endregion
    }

    // Вспомогательные
    partial class Lesson6
    {
        private static int GetSecondNumber(object[] args, int subtask)
        {
            int number = 0;
            int.TryParse(args[1].ToString(), out number);
            return number;
        }

        private static int GetSubtask(object[] args, out string result)
        {
            int subtask = 0;

            result = "";
            if (!int.TryParse(args[0].ToString(), out subtask))
            {
                result += "Дерево построено, однао вывод результата в консоль не предусмотрен";
            }

            return subtask;
        }
    }

    class Node​<T>
    {
        public T Data { get; set; }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }

        public Node<T> Parent { get; set; }

        public int Passed { get; private set; }

        public bool IsChecked
        {
            get { return Passed > 0; }
        }

        public Node(T value)
        {
            Data = value;
        }

        public Node(T value, Node<T> parent): this(value)
        {
            Parent = parent;
        }

        public void Check()
        {
            Passed++;
            Console.WriteLine($"{this} пройден");
        }

        public override string ToString()
        {
            string data = Data.ToString();
            if (data.Length < 2)
            {
                data = $" {data}";
            }

            string result = $"Узел с числом {data} [слева: {{0}}, справа: {{1}}]";

            string left = "--";
            string right = "--";

            if (Left != null)
            {
                left = Left.Data.ToString();
                if (left.Length < 2)
                {
                    left = $" {left}";
                }
            }

            if (Right != null)
            {
                right = Right.Data.ToString();
                if (right.Length < 2)
                {
                    right = $" {right}";
                }
            }

            return string.Format(result, left, right);
        }
    }
}
