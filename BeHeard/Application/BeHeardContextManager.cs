using BeHeard.Core;
using BeHeard.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application
{
    public class BeHeardContextManager : IBeHeardContextManager
    {
        private readonly BeHeardContext _context;
        public BeHeardContextManager(BeHeardContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
            UserProfileRepository = new UserProfileRepository(_context);
            ActivityResultRepository = new ActivityResultRepository(_context);
        }

        public IUserRepository UserRepository { get; private set; }

        public IUserProfileRepository UserProfileRepository { get; private set; }

        public IActivityResultRepository ActivityResultRepository { get; private set; }

        public ISettingsRepository SettingsRepository { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
