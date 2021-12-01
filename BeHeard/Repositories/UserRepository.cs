using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
                .First();
        }

        public User GetUserByEmail(string email)
        {
            // return Context.Set<User>().Where(u => u.Email == email).First();
            return Context.Users.Where(u => u.Email == email).First();
        }

        public User GetUserByUsername(string username)
        {
            // return Context.Set<User>().Where(u => u.Username == username).First();
            return Context.Users.Where(u => u.Username == username).First();
        }
    }
}
