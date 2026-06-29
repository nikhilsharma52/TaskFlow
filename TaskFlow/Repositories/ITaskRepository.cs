using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Enums;
using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public interface ITaskRepository
    {
        void Add(TaskItem task);
        List<TaskItem> GetAll();
        TaskItem? GetById(int id);
        void Update(TaskItem task);
        void Delete(int id);
        List<TaskItem> SearchByTitle(string title);
        List<TaskItem> GetByState(TaskState state);
        Dictionary<TaskState, int> GetStatusReport();

    }
}
