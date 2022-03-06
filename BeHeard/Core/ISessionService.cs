using BeHeard.Application.Models;

namespace BeHeard.Core
{
    public interface ISessionService
    {
        BeHeardSession  Get();
        bool            Delete();
        bool            Save();
        ISessionService Create(User user);
        ISessionService Update(User user);
    }
}
