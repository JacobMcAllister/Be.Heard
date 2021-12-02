using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly BeHeardContextManager _contextManager;
        public SettingsService(BeHeardContext context)
        {
            _contextManager = new BeHeardContextManager(context);
        }

        public Settings GetUserSettings(Guid userId)
        {
            return _contextManager.SettingsRepository.GetUserSettings(userId);
        }

        public Settings GetUserSettings(User user)
        {
            return _contextManager.SettingsRepository.GetUserSettings(user);
        }
    }
}
