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

        public TaskService(IRepository<TaskItem> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }



        public async Task<List<TaskItem>> GetAllAsync()
        {
            return (await _repository.GetAllAsync()).ToList();

            //return await _context.Tasks
            //    .AsNoTracking()
            //    .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);

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
            _repository.Delete(task);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
