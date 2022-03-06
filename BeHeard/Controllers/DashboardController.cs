using BeHeard.Application;
using BeHeard.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Core;
using BeHeard.Services;

//  Comment
namespace BeHeard.Controllers
{
    [BeHeardAuthorize]
    public class DashboardController : Controller
    {
        private readonly IBeHeardContextManager _contextManager;

        public DashboardController(BeHeardContext context)
        {
            _contextManager = new BeHeardContextManager(context);
        }

        public IActionResult Index()
        {
            var service = new SessionService(HttpContext);
            var model = new DashboardViewModel
            {
                Profile = _contextManager.UserProfileRepository.GetUserProfileByUsername(service.Get().Username),
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
            };
            return View(model);
        }
    }
}
