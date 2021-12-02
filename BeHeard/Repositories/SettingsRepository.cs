using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Repositories
{
    public class SettingsRepository : Repository<Settings>, ISettingsRepository
    {
        public SettingsRepository(BeHeardContext context) : base(context)
        {
        }

        public Settings GetUserSettings(Guid userId)
        {
            return Context.Settings.Include(x => x.User).Where(x => x.User.Id == userId).FirstOrDefault();
        }

        public Settings GetUserSettings(User user)
        {
            return Context.Settings.Include(x => x.User).Where(x => x.User == user).FirstOrDefault();
        }
    }
}
