using BeHeard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Core
{
    public interface IUserProfileService
    {
        public UserProfile GetUserProfile(Guid userId);
        public UserProfile GetUserProfile(User user);
    }
}
