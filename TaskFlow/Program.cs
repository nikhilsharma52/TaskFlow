using TaskFlow;
using TaskFlow.Enums;
using TaskFlow.Exceptions;
using TaskFlow.Extensions;
using TaskFlow.Models;
using TaskFlow.Repositories;

namespace TaskFlow
{
    internal class Program
    {
        private static ITaskRepository repository = new InMemoryTaskRepository();
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Task Flow Application");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. List Tasks");
                Console.WriteLine("3. Complete Task");
                Console.WriteLine("4. Search By Title");
                Console.WriteLine("5. Filter By State");
                Console.WriteLine("6. Status Report");
                Console.WriteLine("7. OOP");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string? input = Console.ReadLine();
                if (input == null) continue;
                switch (input)
                {
                    case "1":
                        await AddTask();
                        break;
                    case "2":
                        await ListTasks();
                        break;
                    case "3":
                        await CompleteTask();
                        break;
                    case "4":
                        await SearchByTitle();
                        break;
                    case "5":
                        await FilterByState();
                        break;
                    case "6":
                        await ShowStatusReport();
                        break;
                    case "7":
                        OOPS();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

            static async Task AddTask()
            {
                Console.WriteLine("Enter task title: ");
                string? title = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Task Title cannot be empty");
                    return;
                }

                try
                {
                    List<TaskItem> tasks = await repository.GetAllAsync();
                    TaskItem task = new TaskItem
                    {
                        Id = tasks.Count + 1,
                        Title = title,
                        State = TaskState.Todo,
                        IsDone = false
                    };
                    await repository.AddAsync(task);
                    Console.WriteLine("Task added successfully.");

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            static async Task ListTasks()
            {
                List<TaskItem> tasks = await repository.GetAllAsync();
                if (tasks.Count == 0)
                {
                    Console.WriteLine("No tasks available.");
                    return;
                }
                foreach (var task in tasks)
                {
                    task.Display();
                }
            }

            static async Task CompleteTask()
            {
                Console.WriteLine("Enter task ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid task ID.");
                    return;
                }

                //TaskItem? task = null;
                //foreach(TaskItem t in tasks){
                //    if (t.Id == id)
                //    {
                //        task = t;
                //        break;
                //    }
                //}

                //var task = tasks.FirstOrDefault(t => t.Id == id);

                try
                {
                    TaskItem? task = await repository.GetByIdAsync(id);

                    if (task == null) throw new TaskNotFoundException(id);

                    task.IsDone = true;
                    task.State = TaskState.Done;
                    await repository.UpdateAsync(task);
                    Console.WriteLine("Task Marked as Completed.");
                }
                catch (TaskNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            static async Task SearchByTitle()
            {
                Console.WriteLine("Enter title to Search: ");
                string? title = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title)) return;

                List<TaskItem> tasks = await repository.SearchByTitleAsync(title);

                if (tasks.Count == 0)
                {
                    Console.WriteLine("No matching tasks found.");
                    return;
                }

                Console.WriteLine();

                foreach (TaskItem task in tasks)
                {
                    task.Display();
                }
            }

            static async Task FilterByState()
            {
                Console.WriteLine("1. Todo");
                Console.WriteLine("2. InProgress");
                Console.WriteLine("3. Done");

                Console.WriteLine("Choose State: ");

                string? input = Console.ReadLine();

                TaskState state;

                switch (input)
                {
                    case "1":
                        state = TaskState.Todo;
                        break;
                    case "2":
                        state = TaskState.InProgress;
                        break;
                    case "3":
                        state = TaskState.Done;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        return;
                }

                List<TaskItem> tasks = await repository.GetByStateAsync(state);

                if (tasks.Count == 0)
                {
                    Console.WriteLine("No tasks found.");
                    return;
                }

                Console.WriteLine();

                foreach (TaskItem task in tasks)
                {
                    task.Display();
                }
            }

            static async Task ShowStatusReport()
            {
                Dictionary<TaskState, int> report = await repository.GetStatusReportAsync();
                Console.WriteLine("Status Report:");

                if(report.Count == 0)
                {
                    Console.WriteLine("No tasks available.");
                    return;
                }

                foreach (var item in report)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }

            static async Task OOPS()
            {
                WorkItem bug = new Bug("High");
                WorkItem feature = new Feature("Authentication");

                Console.WriteLine("\n<-- OOP --> ");

                Console.WriteLine(bug.Describe());
                Console.WriteLine(feature.Describe());

                List<TaskItem> tasks = await repository.GetAllAsync();

                TaskItem demoTask = new TaskItem
                {
                    Id = tasks.Count + 1,
                    Title = "Learn Inheritance",
                };

                Console.WriteLine(demoTask.Describe());
            }
        }
    }
}