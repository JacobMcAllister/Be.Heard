﻿using BeHeard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Core
{
    public interface ISettingsService
    {
        Settings GetUserSettings(Guid userId);
        Settings GetUserSettings(User user);
    }
}
