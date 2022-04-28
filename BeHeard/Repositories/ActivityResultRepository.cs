﻿using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Core;
using System.Collections.Generic;
using System.Linq;

namespace BeHeard.Repositories
{
    public class ActivityResultRepository : Repository<ActivityResult>, IActivityResultRepository
    {
        public ActivityResultRepository(BeHeardContext context) : base(context)
        {
        }

        public IEnumerable<ActivityResult> GetActivityResultsByUser(User user)
        {
            return Context.UserProfiles
                .Where(x => x.User.Id == user.Id)
                .First()
                .ActivityResults;
        }

        
        public IEnumerable<ActivityResult> GetFiveChasingResults(User user)
        {
            return
            Context.
            ActivityResults.
            Where(x => x.UserProfileId == user.Id && x.Exercise == Exercise.VolumeChasing).
            Take(5).
            OrderBy(x => x.Date);
        }
        
    }
}
