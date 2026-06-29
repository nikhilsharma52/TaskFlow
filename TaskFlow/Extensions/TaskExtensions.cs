using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Models;

namespace TaskFlow.Extensions
{
    public static class TaskExtensions
    {
        public static void Display(this TaskItem task)
        {
            Console.WriteLine($"ID: {task.Id} | " +
                              $"Title: {task.Title} | " +
                              $"State: {task.State} | " +
                              $"Completed: {task.IsDone}");
        }
    }
}
