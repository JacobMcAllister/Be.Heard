using BeHeard.Application.Models;
using BeHeard.Core;
using BeHeard.Models;
using Microsoft.AspNetCore.Http;

namespace BeHeard.Services
{
    public class SessionService : ISessionService
    {
        private readonly HttpContext _httpContext;
        private BeHeardSession _session;

        public SessionService(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        /// <summary>
        /// Gets the current session.
        /// </summary>
        /// <returns>Returns an instance of BeHeardSession. Unless session is not found then it returns null.</returns>
        public BeHeardSession Get()
        {
            return _httpContext.Session.GetObjectFromJson<BeHeardSession>("session");
        }

        /// <summary>
        /// Delete the current session.
        /// </summary>
        /// <returns>Successful deletion returns true. False otherwise.</returns>
        public bool Delete()
        {
            var session = Get();
            if (session != null)
            {
                _httpContext.Session.Remove("session");
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Saves the BeHeardSession instance created to the context.
        /// </summary>
        /// <returns>True on successful save.</returns>
        public bool Save()
        {
            if (_session != null)
            {
               _httpContext.Session.SetObjectAsJson("session", _session);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Create an instance of BeHeardSession in a temp private variable.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The calling object. Uses fluent pattern with the Save method.</returns>
        public ISessionService Create(User user)
        {
            // NOTE: null values are temporary until architecture is changed to be User centric.
            // User will hold all the information. This will require database changes.
            _session = new BeHeardSession
            {
                Address      = null,
                Age          = user.Age,
                Email        = user.Email,
                FirstName    = user.FirstName,
                Gender       = user.Gender,
                LastName     = user.LastName,
                MasterVolume = 75, // default
                PhoneNumber  = user.PhoneNumber,
                Preferences  = null,
                Subscription = null,
                Username     = user.Username,
            };
            return this;
        }

        /// <summary>
        /// Create an instance of BeHeardSession in a temp private variable.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The calling object. Uses fluent pattern with the Save method.</returns>
        public ISessionService Update(User user)
        {
            return Create(user);
        }
    }
}
