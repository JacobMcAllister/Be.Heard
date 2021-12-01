using BeHeard.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application
{
    public class BeHeardContext : DbContext
    {
        public BeHeardContext(DbContextOptions<BeHeardContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<ActivityResult> ActivityResults { get; set; }
    }
}
