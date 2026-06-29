using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Exceptions
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException(int id) : base($"Task with ID {id} was nto found.")
        {
        }
    }
}
