using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Algorithms
{
    public abstract class BaseLesson : ILesson
    {
        public virtual void DoSomeWork()
        {
            var methods = AttributeHelper.GetMethods<DescriptionAttribute>(this);

            if (methods.Count() <= 0)
            {
                Console.WriteLine("Удовлетворяющих условию задач не найдено");
                return;
            }

            do
            {
                Console.WriteLine();
                DisplayMenu(methods);

                var taskNumber = Console.ReadLine();

                var method = methods.FirstOrDefault(x => x.Name.ToUpper() == $"TASK{taskNumber}");

                if (method == null)
                {
                    Console.WriteLine($"Упс, похоже такого задания (\"{taskNumber}\") еще не задавали!");
                    Console.WriteLine();
                    continue;
                }

                var task = method.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();
                var milestones = method.GetCustomAttributes<MilestoneAttribute>();

                Console.WriteLine("---");
                Console.WriteLine($"\t{task?.Description}:");

                var args = WaitForUserInput(milestones);

                object[] result = Invoke(method, args);

                // Выводим результат
                // Определяем формат вывода результата
                var milestone = milestones.FirstOrDefault(x => x.Milestone == MilestoneAttribute.Milestones.Output);

                if (milestone == null) { continue; }

                if (milestone == null || result == null || result.Count() <= 0)
                {
                    Console.WriteLine("Вывод результата не определен");
                }

                string format = $"{milestone?.Description}";

                if (milestone.WriteTimestampToConsole)
                {
                    format += $" [время выполнения {{{result.Length - 1}:0.000}} мс]";
                }

                // Выводим
                Console.WriteLine(format, result);

            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        private static string[] WaitForUserInput(IEnumerable<MilestoneAttribute> milestones)
        {
            string[] args = { };

            foreach (var m in milestones.Where(x => x.Milestone == MilestoneAttribute.Milestones.Input))
            {
                Console.WriteLine($"{m.Description}");

                if (!m.WaitInput)
                {
                    continue;
                }

                args = args.Concat(Console.ReadLine().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).ToArray();
            }

            return args;
        }

        private static void DisplayMenu(IEnumerable<MethodInfo> methods)
        {
            Console.WriteLine($"----------- Выберите задание (введите от 1 до {methods.Count()}) ----------");

            foreach (var menuItem in methods)
            {
                var description = menuItem.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault()?.Description;
                Console.WriteLine($"\t{description}");
            }

            Console.WriteLine("-----------------------------------------------------------");
        }

        private object[] Invoke(MethodInfo method, string[] args)
        {
            // Засекаем старт задачи
            DateTime timestamp = DateTime.Now;

            // Выполняем задачу
            var result = method.Invoke(this, new object[] { args }) as object[];

            // Вычисляем время выполнения задачи
            TimeSpan timespan = DateTime.Now - timestamp;

            // Добавляем время выполения в конец результирующего массива
            Array.Resize(ref result, result.Length + 1);
            result[result.Length - 1] = timespan.TotalMilliseconds;

            return result;
        }

        public override string ToString()
        {
            var descriptions = AttributeHelper.GetAttributes<DescriptionAttribute>(this);

            return descriptions.FirstOrDefault()?.Description ?? "Безымянный";
        }
    }
}
