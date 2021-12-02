using BeHeard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Core
{
    public interface ISettingsRepository : IRepository<Settings>
    {
        Settings GetUserSettings(Guid userId);
        Settings GetUserSettings(User user);
    }
}
