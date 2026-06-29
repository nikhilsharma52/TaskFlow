using TaskFlow;
using TaskFlow.Enums;
using TaskFlow.Exceptions;
using TaskFlow.Extensions;
using TaskFlow.Models;
using TaskFlow.Repositories;


ITaskRepository repository = new InMemoryTaskRepository();

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
            AddTask();
            break;
        case "2":
            ListTasks();
            break;
        case "3":
            CompleteTask();
            break;
        case "4":
            SearchByTitle();
            break;
        case "5":
            FilterByState();
            break;
        case "6":
            ShowStatusReport();
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
            State = TaskState.Todo,
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
    foreach (var task in repository.GetAll())
    {
        task.Display();
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

    try
    {
        TaskItem? task = repository.GetById(id);

        if (task == null) throw new TaskNotFoundException(id);

        task.IsDone = true;
        task.State = TaskState.Done;
        repository.Update(task);
        Console.WriteLine("Task Completed.");
    }
    catch (TaskNotFoundException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void SearchByTitle()
{
    Console.WriteLine("Enter title to Search: ");
    string? title = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(title)) return;

    List<TaskItem> tasks = repository.SearchByTitle(title);

    if(tasks.Count == 0)
    {
        Console.WriteLine("No matching tasks found.");
        return;
    }

    foreach(TaskItem task in tasks) 
    {
        task.Display();
    }
}

void FilterByState()
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

    List<TaskItem> tasks = repository.GetByState(state);

    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks found.");
        return;
    }

    foreach(TaskItem task in tasks)
    {
        task.Display();
    }
}

void ShowStatusReport()
{
    Dictionary<TaskState, int> report = repository.GetStatusReport();
    Console.WriteLine("Status Report:");
    foreach (var item in report)
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }
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