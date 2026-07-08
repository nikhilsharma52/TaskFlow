using TaskFlow.API.Models;

namespace TaskFlow.API.Services
{
    public class TaskService : ITaskService
    {
        public static List<TaskItem> tasks = new()
        {
            new()
            {
                TaskId = 1,
                ProjectId = 1,
                Title = "Design Database",
                Description = "Create Database Schema",
                IsCompleted = false
            }
        }
    }
}
