using System;
using BeHeard.Application.Models;
using BeHeard.Services;
using Microsoft.AspNetCore.Http;

namespace BeHeard.Application
{
    public class SessionHandler
    {
        public event EventHandler<LoginEventArgs> Login;
        public event EventHandler<EventArgs> Logout;
        private readonly HttpContext _httpContext;

        public SessionHandler(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        protected virtual void OnLogin(User user)
        {
            var service = new SessionService(_httpContext);
            service.Create(user).Save();

            // NOTE: not that useful right now.
            Login?.Invoke(this, new LoginEventArgs(user));
        }

        protected virtual void OnLogout()
        {
            var service = new SessionService(_httpContext);
            service.Delete();

            // NOTE: not that useful right now.
            Logout?.Invoke(this, EventArgs.Empty);
        }
    }

    public class LoginEventArgs : EventArgs
    {
        public User User { get; }

        public LoginEventArgs(User user)
        {
            User = user;
        }
    }
}
