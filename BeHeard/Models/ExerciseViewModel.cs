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

        public List<ActivityResult> results { get; set; }
        public string FullName => $"{User.FirstName} {User.LastName}";

        public int activityTotal => ActivityResults.ElementAt(0).Counter +
            ActivityResults.ElementAt(1).Counter + 
            ActivityResults.ElementAt(2).Counter + 
            ActivityResults.ElementAt(3).Counter;

        /*
        public IEnumerable<ActivityResult> five_VolumeChasingResults => 
            (IEnumerable<ActivityResult>)ActivityResults.
            Take(5).
            Where(x => x.Exercise == Exercise.VolumeChasing).
                Select(x => new
                {
                    x.Date,
                    x.Difficulty,
                    x.Syllable,
                    x.Decibel
                }).
                ToList();
        */
 
    }
}
