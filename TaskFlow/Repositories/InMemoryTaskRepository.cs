using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new();
        public void Add(TaskItem task)
        {
            _tasks.Add(task);
        }
        public List<TaskItem> GetAll()
        {
            return _tasks;
        }
        public TaskItem? GetById(int id)
        {
            return _tasks.Find(t => t.Id == id);
        }
        public void Update(TaskItem updatedtask)
        {
            TaskItem? task = GetById(updatedtask.Id);

            if (task != null)
            {
                task.Title = updatedtask.Title;
                task.IsDone = updatedtask.IsDone;
                task.State = updatedtask.State;
            }
        }
        public void Delete(int id)
        {
            TaskItem? task = GetById(id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }
    }
}
