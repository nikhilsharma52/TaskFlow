using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
