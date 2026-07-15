using TaskFlow.API.Models;
using TaskFlow.API.Repositories;

namespace TaskFlow.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public ProjectService(IRepository<Project> repository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;

        }
        public async Task<List<Project>> GetAllAsync()
        {
            if(_currentUserService.Role == "Admin")
            {
                return (await _repository.GetAllAsync()).ToList();
            }
            return (await _repository.FindAsync(p => p.OwnerUserId == _currentUserService.UserId)).ToList();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);

            if (project == null)
            {
                return null;
            }

            if (_currentUserService.Role != "Admin" && project.OwnerUserId != _currentUserService.UserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to view this project.");
            }

            return project;
        }
        public async Task<Project> CreateAsync(Project project)
        {
            await _repository.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();
            return project;
        }

        public async Task<bool> UpdateAsync(int id, Project project)
        {
            var existingProject = await _repository.GetByIdAsync(id);
            if (existingProject == null)
            {
                return false;
            }

            if (_currentUserService.Role != "Admin" && project.OwnerUserId != _currentUserService.UserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to modify this project.");
            }

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;

            _repository.Update(existingProject);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);
            if (project == null)
            {
                return false;
            }

            if (_currentUserService.Role != "Admin" && project.OwnerUserId != _currentUserService.UserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to modify this project.");
            }

            _repository.Delete(project);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
