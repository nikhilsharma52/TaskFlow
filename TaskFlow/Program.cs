using TaskFlow;
using TaskFlow.Enums;
using TaskFlow.Models;
using TaskFlow.Repositories;


ITaskRepository repository = new InMemoryTaskRepository();

while (true)
{
    Console.WriteLine("Task Flow Application");
    Console.WriteLine("1. Add Task");
    Console.WriteLine("2. List Tasks");
    Console.WriteLine("3. Complete Task");
    Console.WriteLine("4. OOP");
    Console.WriteLine("5. Exit");
    Console.Write("Choose an option: ");

    string? input = Console.ReadLine();
    if (input == null) continue;
    switch (input)
    {
        case "1":
            AddTask();
            break;
        case "2":
            ListTasks();
            break;
        case "3":
            CompleteTask();
            break;
        case "4":
            OOPS();
            break;
        case "5":
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

void AddTask()
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

        TaskItem task = new TaskItem
        {
            Id = repository.GetAll().Count + 1,
            Title = title,
            IsDone = false
        };
        repository.Add(task);
        Console.WriteLine("Task added successfully.");

    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

void ListTasks()
{
    if (repository.GetAll().Count == 0)
    {
        Console.WriteLine("No tasks available.");
        return;
    }
    Console.WriteLine("Tasks:");
    foreach (var task in repository.GetAll())
    {
        string status = task.IsDone ? "Done" : "Pending";
        Console.WriteLine($"[{task.Id}] {task.Title} - {status}");
    }
}

void CompleteTask()
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

    TaskItem? task = repository.GetById(id);

    if (task == null)
    {
        Console.WriteLine("Task not found.");
        return;
    }
    task.IsDone = true;
    task.State = TaskState.Done;
    repository.Update(task);
    Console.WriteLine("Task Completed.");
}

void OOPS()
{
    WorkItem bug = new Bug("High");
    WorkItem feature = new Feature("Authentication");

    Console.WriteLine("\n<-- OOP --> ");

    Console.WriteLine(bug.Describe());
    Console.WriteLine(feature.Describe());

    TaskItem demoTask = new TaskItem
    {
        Id = repository.GetAll().Count + 1,
        Title = "Learn Inheritance",
    };

    Console.WriteLine(demoTask.Describe());
}