
// Бижанов Евгений

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Algorithms
{
    [Description("Урок 7 - Графы. Алгоритмы на графах")]
    public partial class Lesson7 : BaseLesson
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

        [Description("Задача 1 - Функции, которые считывают матрицу смежности")]
        #region
        [Milestone("Матрица смежности:\n\r{0}", MilestoneAttribute.Milestones.Output)]
        public object[] Task1(params object[] args)
        {
            // Входные данные

            // Алгоритм
            var matrix = Task1_GetMatrix(matrixRepresentation);

            // Вывод

            string output = "";
            string mRow;

            var size = Math.Sqrt(matrix.Length);
            for (int row = 0; row < size; row++)
            {
                mRow = "";
                for (int col = 0; col < size; col++)
                {
                    mRow += $" {matrix[row, col]}";
                }
                output += $"    |{mRow.TrimStart()}|\n\r";
            }

            return new object[] { output };
        }
        #endregion

        [Description("Задача 2 - Написать рекурсивную функцию обхода графа в глубину")]
        #region
        [Milestone("-------------------------------------------------------------------------", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("Введите номер вершины с которой нужно начать (или нажмите Enter)", MilestoneAttribute.Milestones.Input)]
        public object[] Task2(params object[] args)
        {
            // Входные данныe
            int start = GetStartVertex(args);

            var matrix = Task1_GetMatrix(matrixRepresentation);

            var size = (int)Math.Sqrt(matrix.Length);
            Vertex[] vertexes = PrepareVertexes(size);

            // Алгоритм
            Task2_GoThroughGraphRecursively(vertexes, vertexes[start - 1], matrix, size);

            // Вывод
            return new object[] { };
        }

        private void Task2_GoThroughGraphRecursively(Vertex[] vertexes, Vertex vertex, int[,] matrix, int size)
        {
            // отмечаемся
            vertex.Check();
            for (int j = 0; j < size; j++)
            {
                if (matrix[vertex.Index, j] > 0)
                {
                    var nextVertex = vertexes[j];

                    // идем дальше, если уже были
                    if (nextVertex.IsChecked) { continue; }

                    Task2_GoThroughGraphRecursively(vertexes, nextVertex, matrix, size);
                }
            }
        }
        #endregion

        [Description("Задача 3 - Написать функцию обхода графа в ширину")]
        #region
        [Milestone("-------------------------------------------------------------------------", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("Введите номер вершины с которой нужно начать (или нажмите Enter)", MilestoneAttribute.Milestones.Input)]
        public object[] Task3(params object[] args)
        {
            // Входные данные
            int start = GetStartVertex(args);

            var matrix = Task1_GetMatrix(matrixRepresentation);

            var size = (int)Math.Sqrt(matrix.Length);
            Vertex[] vertexes = PrepareVertexes(size);

            // Алгоритм
            Task3_GoThroughGraphRecursively(vertexes, vertexes[start - 1], matrix, size);

            // Вывод
            return new object[] { };
        }

        private void Task3_GoThroughGraphRecursively(Vertex[] vertexes, Vertex vertex, int[,] matrix, int size)
        {
            var queue = new Queue<Vertex>();

            queue.Enqueue(vertex);

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();

                if (currentVertex.IsChecked) { continue; }

                currentVertex.Check();

                int i = currentVertex.Index;
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        var nextVertex = vertexes[j];
                        if (!nextVertex.IsChecked)
                        {
                            queue.Enqueue(nextVertex);
                        }
                    }
                }
            }
        }
        #endregion
    }

    // Вспомогательные
    partial class Lesson7
    {
        private int[,] Task1_GetMatrix(string[] mapStrings)
        {
            var size = mapStrings.Length;
            int[,] map = new int[size, size];

            for (int i = 0; i < mapStrings.Length; i++)
            {
                var cols = mapStrings[i].Split(" ".ToCharArray());
                for (int j = 0; j < cols.Length; j++)
                {
                    int value = 0;
                    if (int.TryParse(cols[j], out value))
                    {
                        map[i, j] = value;
                    }
                }
            }

            return map;
        }

        private int GetStartVertex(object[] args)
        {
            int start = 1;
            try
            {
                if (!int.TryParse(args[0].ToString(), out start))
                {
                    start = 1;
                }
            }
            catch { }

            if (start < 1 || start > matrixRepresentation.Length)
            {
                Console.WriteLine($"ВНИМАНИЕ! Номер вершины должен лежать в диапазоне от 1 до {matrixRepresentation.Length}, будет выбрано значение по умолчанию - 1");
                start = 1;
            }

            return start;
        }

        private static Vertex[] PrepareVertexes(int size)
        {
            var vertexes = new Vertex[size];

            for (int i = 0; i < size; i++)
            {
                vertexes[i] = new Vertex(i);
            }

            return vertexes;
        }
    }

    class Vertex
    {
        public int Index { get; private set; }

        public string Label
        {
            get { return $"V{Index + 1}"; }
        }

        public int Passes { get; private set; }

        public bool IsChecked
        {
            get { return Passes > 0; }
        }

        public void Check()
        {
            Passes++;
            Console.WriteLine("     Вершина '" + Label + "' посещена");
        }

        public Vertex(int index)
        {
            Index = index;
        }
    }
}
