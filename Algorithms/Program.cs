using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var lessons = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                          from type in assembly.GetTypes()
                          where type.Name.ToUpper().StartsWith("LESSON")
                          select type;

            if (lessons.Count() <= 0)
            {
                Console.WriteLine("Уроков не найдено, нажмите любую клавишу, что бы выйти");
                Console.ReadKey(true);
                return;
            }

            bool lessonIsSelected = false;
            Type lessonType;

            do
            {
                DisplayMenu(lessons);
                string number = Console.ReadLine();

                lessonType = !string.IsNullOrEmpty(number) ?
                    lessons.FirstOrDefault(x => x.Name.ToUpper() == $"LESSON{number}") :
                    lessons.OrderByDescending(x => x.Name).FirstOrDefault();

                lessonIsSelected = lessonType != null;

                if (!lessonIsSelected)
                {
                    Console.WriteLine($"Не стоит торопить события, урока \"{number}\" пока еще не существует ;)");
                    Console.WriteLine();
                }
            } while (!lessonIsSelected);

            ILesson lessonInstance = Activator.CreateInstance(lessonType) as ILesson;

            Console.WriteLine($"------- {lessonInstance} ----------");

            try
            {
                lessonInstance?.DoSomeWork();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Во время выполнения урока \"{lessonInstance}\" возникла ошибка:");
                Console.WriteLine();
                Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine(ex.StackTrace);
            }

            Console.ReadLine();
        }

        private static void DisplayMenu(IEnumerable<Type> lessons)
        {
            Console.WriteLine($"------- Нажмите ENTER, что бы выбрать текущий урок --------");
            Console.WriteLine($"- Или введите цифру от 1 до {lessons.Count()}, что бы выбрать другой урок -");

            foreach (var menuItem in lessons.OrderBy(x=>x.Name))
            {
                var description = menuItem.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault()?.Description;
                Console.WriteLine($"\t{description}");
            }

            Console.WriteLine("-----------------------------------------------------------");
        }
    }
}
