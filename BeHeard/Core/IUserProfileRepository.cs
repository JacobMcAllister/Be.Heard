using BeHeard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Core
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        UserProfile GetUserProfileByUser(User user);
        UserProfile GetUserProfileByUsername(string username);

    }
}
