using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application.Models
{
    public class Settings
    {
        [ForeignKey("User")]
        public Guid         Id           { get; set; }
        public int          MasterVolume { get; set; } // what is this even used for
        public Preferences  Preferences  { get; set; }
        public Subscription Subscription { get; set; }
        public User         User         { get; set; }
    }
}
