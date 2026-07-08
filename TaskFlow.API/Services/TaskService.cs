using Microsoft.Identity.Client;
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
                Title = "Learn Controllers",
                Description = "Need to learn Controllers and implement them",
                IsCompleted = false
            },
            new()
            {
                TaskId = 2,
                ProjectId = 1,
                Title = "Learn DTOs",
                Description = "Need to learn dtos to impleted it",
                IsCompleted = false
            }
        };

        public List<TaskItem> GetAll()
        {
            return tasks;
        }

        public TaskItem? GetById(int id)
        {
            return tasks.FirstOrDefault(t => t.TaskId == id);
        }

        public TaskItem Create(TaskItem task)
        {
            task.TaskId = tasks.Max(t => t.TaskId) + 1;
            tasks.Add(task);
            return task;
        }

        public bool Update(int id, TaskItem task)
        {
            var existingTask = tasks.FirstOrDefault(t => t.TaskId == id);
            if (existingTask == null)
            {
                return false;
            }
            existingTask.ProjectId = task.ProjectId;
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;
            return true;
        }

        public bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.TaskId == id);
            if (task == null)
            {
                return false;
            }
            tasks.Remove(task);
            return true;
        }
    }
}
