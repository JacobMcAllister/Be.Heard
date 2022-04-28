using BeHeard.Application;
using BeHeard.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Core;
using BeHeard.Services;
using BeHeard.Application.Models;

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
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
            };
            model.ActivityResults = _contextManager.UserRepository.PullExercises(model.User.Id);

            model.ActivityCount = new List<int> { };
            for (int i = 0; i < 5; i++)
            {
                model.ActivityCount.Add(model.ActivityResults.Where(c => c.Exercise == (Exercise)i).Count());

            }

            return View(model);
        }
    }
}
