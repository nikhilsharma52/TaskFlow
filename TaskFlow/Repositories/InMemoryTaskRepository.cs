using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Text;
using System.Text.Json;
using TaskFlow.Enums;
using TaskFlow.Exceptions;
using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private List<TaskItem> _tasks;
        private readonly string _filePath = "Data/tasks.json";

        public InMemoryTaskRepository()
        {
            if(!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }

            string json = File.ReadAllText(_filePath);
            _tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        private async Task SaveTasksAsync()
        {
            string json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task AddAsync(TaskItem task)
        {
            _tasks.Add(task);
            await SaveTasksAsync();
        }
        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await Task.FromResult(_tasks);
        }
        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            TaskItem? task = _tasks.Find(t => t.Id == id);
            return await Task.FromResult(task);
        }
        public async Task UpdateAsync(TaskItem updatedtask)
        {
            TaskItem? task = await GetByIdAsync(updatedtask.Id);

            if (task != null)
            {
                throw new TaskNotFoundException(updatedtask.Id);
            }

            task.Title = updatedtask.Title;
            task.IsDone = updatedtask.IsDone;
            task.State = updatedtask.State;

            await SaveTasksAsync();
        }
        public async Task DeleteAsync(int id)
        {
            TaskItem? task = await GetByIdAsync(id);
            if (task != null)
            {
                throw new TaskNotFoundException(id);
            }

            _tasks.Remove(task);
            await SaveTasksAsync();
        }

        public async Task<List<TaskItem>> SearchByTitleAsync(string title)
        {
            List<TaskItem> result = _tasks.Where(t => t.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            return await Task.FromResult(result);

        }

        public async Task<List<TaskItem>> GetByStateAsync(TaskState state)
        {
            List<TaskItem> result = _tasks.Where(t => t.State == state).ToList();
            return await Task.FromResult(result);
        }

        public async Task<Dictionary<TaskState, int>> GetStatusReportAsync()
        {
            Dictionary<TaskState, int> report = _tasks.GroupBy(t => t.State).ToDictionary(g => g.Key, g => g.Count());
            return await Task.FromResult(report);
        }

    }
}
