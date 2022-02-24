using BeHeard.Application;
using BeHeard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            //var user = _beHeardContextManager.UserRepository.GetUserByUsername("AlbertRules");
            //var userProfile = _beHeardContextManager.UserProfileRepository.GetUserProfileByUser(user);
            //var model = new ProfileModel { UserProfile = userProfile };           


            //var user = _beHeardContextManager.UserRepository.GetUserByUsername("AlbertRules");
            //var userProfile = _beHeardContextManager.UserProfileRepository.GetUserProfileByUser(user);

            //HttpContext.Session.SetString("FirstName", userProfile.User.FirstName);
            //HttpContext.Session.SetString("LastName", userProfile.User.LastName);
            //HttpContext.Session.SetString("Email", userProfile.User.Email);
            //HttpContext.Session.SetString("Username", userProfile.User.Username);

            //SessionModel thisSession = new SessionModel()
            //{
            //    FirstName = HttpContext.Session.GetString("FirstName"),
            //    LastName = HttpContext.Session.GetString("LastName"),
            //    FullName = HttpContext.Session.GetString("FirstName") + HttpContext.Session.GetString("LastName"),
            //    Email = HttpContext.Session.GetString("Email"),
            //    Username = HttpContext.Session.GetString("Username"),
            //    City = "Reno"
            //};

            var thisSession = HttpContext.Session.GetObjectFromJson<SessionModel>("thisSession");


            return View(thisSession);
            //return View(model);
        }
    }
}
