using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application.Models
{
    public class Activity
    {
        // public Guid Id { get; set; }
        string Prompt { get; set; }
        public ActivityLevel Level { get; set; }
    }

    public enum ActivityLevel
    {
        Beginner, Intermediate, Advanced
    }
}
