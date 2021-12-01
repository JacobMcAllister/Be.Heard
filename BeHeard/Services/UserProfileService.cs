using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Services
{
    public class UserProfileService : IUserProfileService
    {
        // private readonly BeHeardContext _context;
        private readonly BeHeardContextManager _contextManager;

        public UserProfileService(BeHeardContext context)
        {
            _contextManager = new BeHeardContextManager(context);
        }

        public UserProfile GetUserProfile(Guid userId)
        {
            return _contextManager.UserProfileRepository.Get(userId);
        }

        public UserProfile GetUserProfile(User user)
        {
            return _contextManager.UserProfileRepository.GetUserProfileByUser(user);
        }
    }
}
