using BeHeard.Application;
using BeHeard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Services;

namespace BeHeard.Controllers
{
    public class ProfileController : Controller
    {
        private readonly BeHeardContextManager _beHeardContextManager;
        public ProfileController(BeHeardContext context)
        {
            _beHeardContextManager = new BeHeardContextManager(context);
        }
        public IActionResult Index()
        {
            var sessionService = new SessionService(HttpContext);
            var user = _beHeardContextManager.UserRepository.GetUserByUsername(sessionService.Get().Username);
            var userProfile = _beHeardContextManager.UserProfileRepository.GetUserProfileByUser(user);
            var model = new ProfileViewModel { UserProfile = userProfile };

            return View(model);
        }
    }
}
