using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Enums;

namespace TaskFlow.Models
{
    public class TaskItem : WorkItem
    {
        private string _title = "";

        public string Title
        {
            get { return _title; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new AggregateException("Title cannot be empty");
                }

                _title = value;
            }
        }

        public bool IsDone { get; set; }

        public TaskState State { get; set; } = TaskState.Todo;


        public bool IsCompleted
        {
            get { return IsDone; }
        }

        public override string Describe()
        {
            return $"Task: {Title}";
        }

    }
}
