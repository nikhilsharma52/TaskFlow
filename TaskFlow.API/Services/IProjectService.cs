using TaskFlow.API.Models;

namespace TaskFlow.API.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project> CreateAsync(Project task);
        Task<bool> UpdateAsync(int id, Project task);
        Task<bool> DeleteAsync(int id);
    }
}
