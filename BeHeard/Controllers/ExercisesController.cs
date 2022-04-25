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
        public IActionResult UpdateDBwResults(User user, ActivityResult activityresult, string Decibel, string viewExercise, string viewSyllable, string viewDifficulty, string viewCategory, int SentenceSet)
        {
            IEnumerable<ActivityResult> results = _contextManager.ActivityResultRepository.GetActivityResultsByUser(user);
            activityresult = results.FirstOrDefault();

            activityresult.Date = DateTime.Now;
            activityresult.Counter = 0;
            activityresult.Decibel = Decibel;
            activityresult.SentenceSet = SentenceSet;

            switch(viewExercise)
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
                case "RoteSpeech":
                    activityresult.Exercise = Exercise.Rote;
                    break;
            }
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
                case "Impossible":
                    activityresult.Difficulty = ActivityLevel.Impossible;
                    break;
            };
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
