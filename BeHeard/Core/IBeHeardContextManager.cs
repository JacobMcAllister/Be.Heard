using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Core
{
    public interface IBeHeardContextManager : IDisposable
    {
        IUserRepository UserRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        IActivityResultRepository ActivityResultRepository { get; }
        ISettingsRepository SettingsRepository { get; }
        int SaveChanges();
    }
}
