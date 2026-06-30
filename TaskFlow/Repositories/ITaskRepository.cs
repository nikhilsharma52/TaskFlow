using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Enums;
using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem task);
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(int id);
        Task<List<TaskItem>> SearchByTitleAsync(string title);
        Task<List<TaskItem>> GetByStateAsync(TaskState state);
        Task<Dictionary<TaskState, int>> GetStatusReportAsync();

    }
}
