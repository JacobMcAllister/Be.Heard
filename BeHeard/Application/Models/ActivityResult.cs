using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application.Models
{
    public class ActivityResult
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Frequency { get; set; }
        public double Decibel { get; set; }
        public int Counter { get; set; }
        public ActivityLevel Level { get; set; }
    }
}
