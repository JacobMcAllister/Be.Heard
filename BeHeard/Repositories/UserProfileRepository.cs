using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BeHeard.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(BeHeardContext context) : base(context)
        {
        }

        public UserProfile GetUserProfileByUser(User user)
        {
            //return Context.Set<UserProfile>().Where(x => x.User.Id == user.Id).First();
            //return Context.UserProfiles.Where(x => x.User.Id == user.Id).First();
            return Context.UserProfiles
                .Include(x => x.Settings)
                .Include(x => x.ActivityResults)
                .First(x => x.User== user);
        }

    }
}
