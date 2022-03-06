using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BeHeard.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BeHeardContext context) : base(context)
        {
        }

        public User GetUserByCredentials(AuthorizationRequest credentials)
        {
            //return Context.Set<User>()
            //    .Where(u => u.Username == credentials.Username && u.Password == credentials.Password)
            //    .First();

            return Context.Users
                .Where(u => u.Username == credentials.Username && u.Password == credentials.Password)
                .FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            // return Context.Set<User>().Where(u => u.Email == email).First();
            return Context.Users.Where(u => u.Email == email).First();
        }

        public User GetUserByUsername(string username)
        {
            // return Context.Set<User>().Where(u => u.Username == username).First();
            // return Context.Users.Where(u => u.Username == username).FirstOrDefault();
            return Context.Users
                .Include(x => x.Settings)
                .Include(x => x.Settings.Preferences)
                .Include(x => x.Settings.Subscription)
                .Include(x => x.Profile)
                .Include(x => x.Address)
                .First(x => x.Username == username);
        }
    }
}
