using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application.Models
{
    public class Settings
    {
        public Guid         Id           { get; set; }
        public int          MasterVolume { get; set; } // what is this even used for
        public Preferences  Preferences  { get; set; }
        public Subscription Subscription { get; set; }
        public User         User         { get; set; }
    }
}
