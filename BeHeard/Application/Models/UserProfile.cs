using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application.Models
{
    public class UserProfile
    {
        [ForeignKey("User")]
        public Guid Id { get; set; }
        public User User { get; set; }
        public Settings Settings { get; set; } // NOTE: UserProfile does not interact with Settings. Consider deleting.
        public List<ActivityResult> ActivityResults { get; set; }
        public List<RecordingRecord> RecordingRecords { get; set; }
    }
}
