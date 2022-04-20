using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Application.Models;

namespace BeHeard.Models
{
    public class ExerciseViewModel
    {
        public User User { get; set; }
        public IEnumerable<ActivityResult> ActivityResults { get; set; }
        public string FullName => $"{User.FirstName} {User.LastName}";

        public int activityTotal => ActivityResults.ElementAt(0).Counter +
            ActivityResults.ElementAt(1).Counter + 
            ActivityResults.ElementAt(2).Counter + 
            ActivityResults.ElementAt(3).Counter;
    }
}
