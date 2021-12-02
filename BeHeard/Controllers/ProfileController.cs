using BeHeard.Application;
using BeHeard.Models;
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
            var user = _beHeardContextManager.UserRepository.GetUserByUsername("AlbertRules");
            var userProfile = _beHeardContextManager.UserProfileRepository.GetUserProfileByUser(user);
            var model = new ProfileModel { UserProfile = userProfile };           
            
            
            return View(model);
        }
    }
}
