using TaskFlow.API.Models;
using TaskFlow.API.Repositories;

namespace TaskFlow.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IRepository<Project> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Project>> GetAllAsync()
        {
            return (await _repository.GetAllAsync()).ToList();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<Project> CreateAsync(Project task)
        {
            await _repository.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateAsync(int id, Project project)
        {
            var existingProject = await _repository.GetByIdAsync(id);
            if (existingProject == null)
            {
                return false;
            }
            existingProject.Name = project.Name;
            existingProject.Description = project.Description;

            _repository.Update(existingProject);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
            {
                return false;
            }
            _repository.Delete(task);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
