using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Application;
using BeHeard.Core;
using BeHeard.Models;
using BeHeard.Services;

namespace BeHeard.Controllers
{
    [BeHeardAuthorize]
    public class ExercisesController : Controller
    {
        private readonly IBeHeardContextManager _contextManager;

        public ExercisesController(BeHeardContext context)
        {
            _contextManager = new BeHeardContextManager(context);
        }

        public IActionResult Index()
        {
            var service = new SessionService(HttpContext);
            var model = new ExerciseViewModel
            {
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
            };
            return View(model);
        }

        public IActionResult Exercise1()
        {
            return View();
        }

        public IActionResult Exercise2()
        {
            var service = new SessionService(HttpContext);
            var model = new ExerciseViewModel
            {
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
            };
            return View(model);
        }

        public IActionResult Exercise3()
        {
            return View();
        }
    }
}
