using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Models
{
    public class Bug : WorkItem
    {
        public string Severity { get; set; }

        public Bug(string severity)
        {
            Severity = severity;
        }

        public override string Describe()
        {
            return $"Bug: {Severity}";
        }
    }
}
