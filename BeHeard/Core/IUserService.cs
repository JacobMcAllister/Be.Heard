﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Core
{
    public interface IUserService
    {
        bool IsAnExistingUser(string username);
        bool IsValidUserCredentials(string username, string password);
        string GetUserRole(string username);
    }
}
