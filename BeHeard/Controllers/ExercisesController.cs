using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Models;
using Microsoft.AspNetCore.Http;
using BeHeard.Core;
using System.Security.Claims;
using BeHeard.Application;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Cryptography;
using BeHeard.Core;
using System.Security.Claims;
using BeHeard.Application;
using BeHeard.Application.Helpers;
using BeHeard.Application.Models;
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

        public IActionResult volume_chasing()
        {
            var service = new SessionService(HttpContext);
            var model = new ExerciseViewModel
            {
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
            };
            return View(model);
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
            var service = new SessionService(HttpContext);
            var model = new ExerciseViewModel
            {
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
            };
            return View(model);
        }

        public IActionResult breathing()
        {
            var service = new SessionService(HttpContext);
            var model = new ExerciseViewModel
            {
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
            };
            return View(model);
        }

        public IActionResult volume_phrasing()
        {
            var service = new SessionService(HttpContext);
            var model = new ExerciseViewModel
            {
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
            };
            return View(model);
        }

        public IActionResult rote_speech()
        {
            var service = new SessionService(HttpContext);
            var model = new ExerciseViewModel
            {
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateDBwResults(User user, ActivityResult activityresult)
        {
            IEnumerable<ActivityResult> results = _contextManager.ActivityResultRepository.GetActivityResultsByUser(user);
            activityresult = results.FirstOrDefault();

            





            try
            {
                _contextManager.ActivityResultRepository.Add(activityresult);
                _contextManager.SaveChanges();
                return new EmptyResult();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
