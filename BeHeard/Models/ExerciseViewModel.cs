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
    }
}
