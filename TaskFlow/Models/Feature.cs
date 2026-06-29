using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Models
{
    public class Feature : WorkItem
    {
        public string Module { get; set; }

        public Feature(string module)
        {
            Module = module;
        }

        public override string Describe()
        {
            return $"Feature: {Module}";
        }
    }
}
