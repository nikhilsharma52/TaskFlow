using TaskFlow.API.Models;

namespace TaskFlow.API.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<bool> UpdateAsync(int id, TaskItem task);
        Task<bool> DeleteAsync(int id);
    }
}
