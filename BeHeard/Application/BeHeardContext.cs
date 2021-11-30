﻿using BeHeard.Models;
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

        public DbSet<UserModel> Users { get; set; }
    }
}