using System.Collections.Generic;
using BeHeard.Application.Helpers;
using BeHeard.Application.Models;

namespace BeHeard.Models
{
    public class AnalyticsViewModel
    {
        public int count;

        public IEnumerable<ActivityResult> ActivityResults { get; set; }

        public List<int> ActivityCount { get; set; }
        public List<int> ActivityDifficultyCount { get; set; }

    }
}
