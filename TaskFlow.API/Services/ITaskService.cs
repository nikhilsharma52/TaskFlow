using TaskFlow.API.Models;

namespace TaskFlow.API.Services
{
    public interface ITaskService
    {
        List<TaskItem> GetAll();
        TaskItem? GetById(int id);
        TaskItem Create(TaskItem task);
        bool update (int id, TaskItem task);
        bool delete (int id);
    }
}
