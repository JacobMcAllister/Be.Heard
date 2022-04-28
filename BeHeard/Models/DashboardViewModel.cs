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
        //public UserProfile Profile { get; set; }
        public IEnumerable<ActivityResult> ActivityResults { get; set; }
        public List<int> ActivityCount { get; set; }
        public string FullName => $"{User.FirstName} {User.LastName}";

    }
}
