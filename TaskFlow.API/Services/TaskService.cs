using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.Models;

namespace TaskFlow.API.Services
{
    public class TaskService : ITaskService
    {

        private readonly TaskFlowDbContext _context;

        public TaskService(TaskFlowDbContext context)
        {
            _context = context;
        }



        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TaskId == id);
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateAsync(int id, TaskItem task)
        {
            var existingTask = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == id);
            if (existingTask == null)
            {
                return false;
            }
            existingTask.ProjectId = task.ProjectId;
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == id);
            if (task == null)
            {
                return false;
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
