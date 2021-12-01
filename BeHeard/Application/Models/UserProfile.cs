using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Settings Settings { get; set; }
        public List<ActivityResult> ActivityResults { get; set; }
    }
}
