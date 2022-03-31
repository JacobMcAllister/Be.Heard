using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Application.Models;
using Microsoft.Extensions.Primitives;

namespace BeHeard.Models
{
    public class DashboardViewModel
    {
        public User User { get; set; }
        public UserProfile Profile { get; set; }
        public string FullName => $"{User.FirstName} {User.LastName}";

        public int activityTotal => Profile.ActivityResults[0].Counter +
            Profile.ActivityResults[1].Counter +
            Profile.ActivityResults[2].Counter +
            Profile.ActivityResults[3].Counter;

    }
}
