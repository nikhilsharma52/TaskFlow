using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.Models;
using TaskFlow.API.Repositories;

namespace TaskFlow.API.Services
{
    public class TaskService : ITaskService
    {

        private readonly IRepository<TaskItem> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public TaskService(IRepository<TaskItem> repository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }



        public async Task<List<TaskItem>> GetAllAsync()
        {
            if (_currentUserService.Role == "Admin")
            {
                return (await _repository.GetAllAsync()).ToList();
            }
            return (await _repository.FindAsync(t => t.AssignedUserId == _currentUserService.UserId)).ToList();

            //return await _context.Tasks
            //    .AsNoTracking()
            //    .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
            {
                return null;
            }

            if (_currentUserService.Role != "Admin" && task.AssignedUserId != _currentUserService.UserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to view this task.");
            }

            return task;

            //return await _context.Tasks
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(t => t.TaskId == id);
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            await _repository.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();
            return task;

            //_context.Tasks.Add(task);
            //await _context.SaveChangesAsync();
            //return task;
        }

        public async Task<bool> UpdateAsync(int id, TaskItem task)
        {
            var existingTask = await _repository.GetByIdAsync(id);
            if (existingTask == null)
            {
                return false;
            }

            if (_currentUserService.Role != "Admin" && task.AssignedUserId != _currentUserService.UserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to modify this Task.");
            }
            
            existingTask.ProjectId = task.ProjectId;
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;

            _repository.Update(existingTask);
            await _unitOfWork.SaveChangesAsync();
            return true;


            //var existingTask = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == id);
            //if (existingTask == null)
            //{
            //    return false;
            //}
            //existingTask.ProjectId = task.ProjectId;
            //existingTask.Title = task.Title;
            //existingTask.Description = task.Description;
            //existingTask.IsCompleted = task.IsCompleted;

            //await _context.SaveChangesAsync();
            //return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var task = await _repository.GetByIdAsync(id);
            if (task == null)
            {
                return false;
            }

            if (_currentUserService.Role != "Admin" && task.AssignedUserId != _currentUserService.UserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to modify this Task.");
            }
            _repository.Delete(task);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
