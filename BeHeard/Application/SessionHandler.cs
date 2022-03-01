using System;
using BeHeard.Application.Models;

namespace BeHeard.Application
{
    public class SessionHandler
    {
        public event EventHandler<LoginEventArgs> Login;
        public event EventHandler<EventArgs> Logout;

        public SessionHandler()
        {

        }

        public void Create(User user, Settings settings)
        {

        }

        public void Update(User user, Settings settings)
        {

        }

        public void Delete()
        {
            OnLogout();
        }

        protected virtual void OnLogin(User user, Settings settings)
        {
            Login?.Invoke(this, new LoginEventArgs(user, settings));
        }

        protected virtual void OnLogout()
        {
            Logout?.Invoke(this, EventArgs.Empty);
        }
    }

    public class LoginEventArgs : EventArgs
    {
        public User User { get; }
        public Settings Settings { get; }

        public LoginEventArgs(User user, Settings settings)
        {
            User = user;
            Settings = settings;
        }
    }
}
