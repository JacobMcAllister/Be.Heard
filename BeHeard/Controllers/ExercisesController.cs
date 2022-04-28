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
            var user = _contextManager.UserRepository.GetUserByUsername(service.Get().Username);
            var model = new ExerciseViewModel
            {
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
                ActivityResults = _contextManager.ActivityResultRepository.GetFiveExerciseResults(user, 0)
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
            var user = _contextManager.UserRepository.GetUserByUsername(service.Get().Username);
            var model = new ExerciseViewModel
            {
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
                ActivityResults = _contextManager.ActivityResultRepository.GetFiveExerciseResults(user, 1)
            };
            return View(model);
        }

        public IActionResult volume_phrasing()
        {
            var service = new SessionService(HttpContext);
            var user = _contextManager.UserRepository.GetUserByUsername(service.Get().Username);
            var model = new ExerciseViewModel
            {
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
                ActivityResults = _contextManager.ActivityResultRepository.GetFiveExerciseResults(user, 2)
            };
            return View(model);
        }

        public IActionResult rote_speech()
        {
            var service = new SessionService(HttpContext);
            var user = _contextManager.UserRepository.GetUserByUsername(service.Get().Username);
            var model = new ExerciseViewModel
            {
                User = _contextManager.UserRepository.GetUserByUsername(service.Get().Username),
                ActivityResults = _contextManager.ActivityResultRepository.GetFiveExerciseResults(user, 3)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateDBwResults(string Decibel, string viewExercise, string viewSyllable, string viewDifficulty, string viewCategory, int SentenceSet)
        {
            var activityresult = new ActivityResult();

            var service = new SessionService(HttpContext);
            var user = _contextManager.UserRepository.GetUserByUsername(service.Get().Username);
            //IEnumerable<ActivityResult> results = _contextManager.ActivityResultRepository.GetActivityResultsByUser(user);
            //List<ActivityResult> results_ToList = results.ToList();

            activityresult.Date = DateTime.Now;
            activityresult.SentenceSet = SentenceSet;
            activityresult.Decibel = Decibel;
            activityresult.Counter = 1;
            switch (viewDifficulty)
            {
                case "Easy":
                    activityresult.Difficulty = ActivityLevel.Easy;
                    break;
                case "Medium":
                    activityresult.Difficulty = ActivityLevel.Medium;
                    break;
                case "Hard":
                    activityresult.Difficulty = ActivityLevel.Hard;
                    break;
                case "Extreme":
                    activityresult.Difficulty = ActivityLevel.Extreme;
                    break;
            };
            switch (viewSyllable)
            {
                case "A":
                    activityresult.Syllable = Syllable.A;
                    break;
                case "E":
                    activityresult.Syllable = Syllable.E;
                    break;
                case "O":
                    activityresult.Syllable = Syllable.O;
                    break;
                case "U":
                    activityresult.Syllable = Syllable.U;
                    break;
                case "NONE":
                    activityresult.Syllable = Syllable.NONE;
                    break;
            }
            switch (viewExercise)
            {
                case "VolumeChasing":
                    activityresult.Exercise = Exercise.VolumeChasing;
                    break;
                case "Breathing":
                    activityresult.Exercise = Exercise.Breathing;
                    break;
                case "Phrasing":
                    activityresult.Exercise = Exercise.Phrasing;
                    break;
                case "Rote":
                    activityresult.Exercise = Exercise.Rote;
                    break;
            }
            switch (viewCategory)
            {
                case "Cities":
                    activityresult.Category = Category.Cities;
                    break;
                case "Directions":
                    activityresult.Category = Category.Directions;
                    break;
                case "PhoneNumbers":
                    activityresult.Category = Category.PhoneNumbers;
                    break;
                case "CommonRequests":
                    activityresult.Category = Category.CommonRequests;
                    break;
                case "MealOrders":
                    activityresult.Category = Category.MealOrders;
                    break;
                case "NONE":
                    activityresult.Category = Category.NONE;
                    break;
            }
            activityresult.UserProfileId = user.Id;
            //results_ToList.Add(activityresult);
            //IEnumerable<ActivityResult> finalResults = results_ToList.AsEnumerable();

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
