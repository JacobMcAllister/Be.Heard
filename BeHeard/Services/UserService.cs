using BeHeard.Application.Models;
using BeHeard.Core;
using BeHeard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Services
{
    public class UserService : IUserService
    {
        // mock user
        // NOTE: add db here
        private List<User> _users = new List<User>
        {
            new User {  FirstName = "super", LastName = "user", Username = "admin", Password = "password" }
        };

        public string GetUserRole(string username)
        {
            if (!IsAnExistingUser(username))
            {
                return string.Empty;
            }

            if (username == "admin")
            {
                return UserRoles.Admin;
            }

            return UserRoles.BasicUser;
        }

        public bool IsAnExistingUser(string username)
        {
            var user = _users.FirstOrDefault(x => x.Username == username);
            if (user != null) return true;
            else return false;
        }

        public bool IsValidUserCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            if (string.IsNullOrWhiteSpace(password))
                return false;

            var user = _users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user != null) return true;
                // return _users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            else return false;
        }
    }

    public static class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string BasicUser = nameof(BasicUser);
    }
}
