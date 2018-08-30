
// Бижанов Евгений

using System;
using System.ComponentModel;
using System.Text;

namespace Algorithms
{
    [Description("Урок 4 - Динамическое программирование. Поиск возвратом")]
    public partial class Lesson4 : BaseLesson
    {
        [Description("Задача 1 - Количество маршрутов с препятствиями")]
        #region
        [Milestone("Кол-во найденных маршрутов, для заданной карты - {0}:\n\r{1}", MilestoneAttribute.Milestones.Output)]
        public object[] Task1(params object[] args)
        {
            // Входные данные
            //
            // Карта:
            //

            string[] mapStrings = new string[]
            {
                "1 1 1",
                "0 1 0",
                "0 1 1"
            };

            var map = GetRoadmap(mapStrings);

            // Алгоритм
            int length = (int)Math.Sqrt(map.Length) - 1;
            int sum = Task1_GetRouteCount(map, length, length);

            // Вывод

            string matrix = "";
            foreach (var s in mapStrings)
            {
                matrix += $"    |{s}|\n\r";
            }

            return new object[] { sum, matrix };
        }

        // Нахождение кол-ва маршрутов обратным поиском
        private int Task1_GetRouteCount(bool[,] roadmap, int a, int b)
        {
            //
            // W(a, b) = W(a, b - 1) + W(a - 1, b), если Map(a, b) = 1
            // W(a, b) = 0, если Map(a, b) = 0
            //
            // a = 0 или b = 0 W(a, b) = 1, если Map(a, b) = 1
            //

            int sum = 0;
            if (roadmap[a, b])
            {
                if ((a <= 0) || (b <= 0))
                {
                    return 1;
                }

                int newA = (a - 1) < 0 ? 0 : a - 1;
                int newB = (b - 1) < 0 ? 0 : b - 1;

                sum += Task1_GetRouteCount(roadmap, a, newB) + Task1_GetRouteCount(roadmap, newA, b);
            }
            else
            {
                return 0;
            }

            return sum;
        }
        #endregion

        [Description("Задача 2 - Задача о нахождении длины максимальной последовательности")]
        #region
        [Milestone("-------------------------------------------------------------------------", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("Введите первую последовательность", MilestoneAttribute.Milestones.Input)]
        [Milestone("Введите вторую последовательность", MilestoneAttribute.Milestones.Input)]
        [Milestone("-------------------------------------------------------------------------", MilestoneAttribute.Milestones.Input, WaitInput = false)]
        [Milestone("    Длина наибольшей общей подпоследовательности '{0}' - {1}", MilestoneAttribute.Milestones.Output)]
        public object[] Task2(params object[] args)
        {
            // Входные данные
            //
            //var sequence1 = "GEEKBRAINS";
            //var sequence2 = "GEEKMINDS";
            //

            var sequence1 = args[0].ToString();
            var sequence2 = args[1].ToString();

            // Алгоритм
            var result = Task2_LengthOfSubsequence(sequence1, sequence2);

            // Вывод
            return new object[] { result.Item1, result.Item2 };
        }

        // Начал было заполнять матрицу, и тут заметил, что для решения
        // данной задачи достаточно просто инкрементировать счетчик
        // В комментариях оставил матрицу, но решение не реализовано полностью
        private Tuple<string, int> Task2_LengthOfSubsequence(string a, string b)
        {
            var sequence1 = new StringBuilder(a);
            var sequence2 = new StringBuilder(b);

            //int[,] matrix = new int[sequence1.Length, sequence2.Length];

            int length = 0;
            string subsequence = "";
            for (int i = 0; i < sequence1.Length; i++)
            {
                for (int j = 0; j < sequence2.Length; j++)
                {
                    if (sequence1[i] == sequence2[j])
                    {
                        subsequence += sequence1[i];
                        length++;

                        if (i < sequence1.Length - 1) i++;
                        //int prevI = (j - 1) < 0 ? i - 1 : i;
                        //int prevj = (j - 1) < 0 ? 0 : j - 1;
                        //prevI = prevI < 0 ? 0 : prevI;
                        //matrix[i, j] = matrix[prevI, prevj] + 1;
                    }
                }
            }

            return new Tuple<string, int>(subsequence, length);
        }
        #endregion

        [Description("Задача 3 - Обойти конем шахматную доску (NxM), пройдя через все поля по одному разу")]
        #region
        [Milestone("{0}", MilestoneAttribute.Milestones.Output)]
        public object[] Task3(params object[] args)
        {
            // Входные данные
            board = new int[N, M];

            // Алгоритм
            Task3_SearchSolution(1);

            // Вывод
            string result = "\n\r";
            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < M; col++)
                {
                    var separator = "    ";
                    if (board[row, col] > 9)
                    {
                        separator = "   ";
                    }
                    result += $"{separator}{board[row, col]}";
                }
                result += "\n\r\n\r";
            }

            return new object[] { result };
        }

        int N = 5; // строки
        int M = 5; // столбцы

        int[,] board;

        // Бинарный поиск
        private bool Task3_SearchSolution(int n)
        {
            if (!CheckBoard(n - 1)) return false;

            // выход
            if (n >= N * M) return true;

            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < M; col++)
                {
                    if (board[row, col] == 0)
                    {
                        board[row, col] = n;
                        Console.Write(".");

                        if (Task3_SearchSolution(n + 1)) return true;

                        Console.Write("\b");
                        board[row, col] = 0;
                    }
                }
            }

            return false;
        }

        private bool CheckBoard(int n)
        {
            if (n <= 1) return true;

            var prevX = 0;
            var prevY = 0;
            var currX = 0;
            var currY = 0;
            var counter = 0;

            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < M; col++)
                {
                    if (board[row, col] == n - 1)
                    {
                        prevX = row;
                        prevY = col;
                        counter++;
                    }

                    if (board[row, col] == n)
                    {
                        currX = row;
                        currY = col;
                        counter++;
                    }

                    if (counter < 2) continue;

                    var checkX = Math.Abs(currX - prevX);
                    var checkY = Math.Abs(currY - prevY);

                    var check = (checkX + checkY) == 3 && (checkX <= 2 && checkY <= 2);

                    if (!check)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion
    }

    // Вспомогательные
    partial class Lesson4
    {
        private bool[,] GetRoadmap(string[] mapStrings)
        {
            var size = mapStrings.Length;
            bool[,] map = new bool[size, size];

            for (int i = 0; i < mapStrings.Length; i++)
            {
                var cols = mapStrings[i].Split(" ".ToCharArray());
                for (int j = 0; j < cols.Length; j++)
                {
                    int value = 0;
                    if (int.TryParse(cols[j], out value))
                    {
                        map[i, j] = value > 0;
                    }
                }
            }

            return map;
        }
    }
}
