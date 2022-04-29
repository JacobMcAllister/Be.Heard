using System.Collections.Generic;
using BeHeard.Application.Helpers;
using BeHeard.Application.Models;

namespace BeHeard.Models
{
    public class AnalyticsViewModel
    {
        public int count;
        public List<State> States = Localization.Los;

        public List<int> stateCount { get; set; }
        public List<int> ageCountMale { get; set; }
        public List<int> ageCountFemale { get; set; }
        public IEnumerable<ActivityResult> ActivityResults { get; set; }

        public List<int> ActivityCount { get; set; }

    }
}
