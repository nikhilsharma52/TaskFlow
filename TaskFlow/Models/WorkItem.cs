using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Models
{
    public class WorkItem
    {
        public int Id { get; set; }

        public virtual string Describe()
        {
            return $"WorkItem ID: {Id}";
        }
    }
}
