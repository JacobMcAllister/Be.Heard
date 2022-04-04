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
            try
            {
                return Context.Users
                    .Where(u => u.Username == credentials.Username && u.Password == credentials.Password)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get user " + credentials.Username + " by credentials becasue of " + e);
                return null;
            }
        }

        public User GetUserByEmail(string email)
        {
            // return Context.Set<User>().Where(u => u.Email == email).First();
            try
            {
                return Context.Users.Where(u => u.Email == email).First();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get user by email (" + email + ") becasue of " + e);
                return null;
            }
        }

        public User GetUserByUsername(string username)
        {
            // return Context.Set<User>().Where(u => u.Username == username).First();
            // return Context.Users.Where(u => u.Username == username).FirstOrDefault();
            try
            {
                return Context.Users
                    .Include(x => x.Settings)
                    .Include(x => x.Settings.Preferences)
                    .Include(x => x.Settings.Subscription)
                    .Include(x => x.Profile)
                    .Include(x => x.Address)
                    .First(x => x.Username == username);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get user " + username + " by username becasue of " + e);
                return null;
            }
        }
    }
}
