using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
