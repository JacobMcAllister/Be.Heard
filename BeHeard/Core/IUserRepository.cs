using BeHeard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Core
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        User GetUserByCredentials(AuthorizationRequest credentials);
    }
}
