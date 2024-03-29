﻿using BeHeard.Application;
using BeHeard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Services;
using BeHeard.Application.Models;

namespace BeHeard.Controllers
{
    [BeHeardAuthorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BeHeardContextManager _beHeardContextManager;     

        public HomeController(ILogger<HomeController> logger, BeHeardContext context)
        {
            _logger = logger;
            _beHeardContextManager = new BeHeardContextManager(context);
        }

        public IActionResult Index()
        {

            var service = new SessionService(HttpContext);

            var session = service.Get();
            var model = new HomeViewModel
            {
                FirstName = session.FirstName,
                LastName = session.LastName,
                //User = _beHeardContextManager.UserRepository.GetUserByUsername(session.Username),
            };
            return View(model);
        }
        public IActionResult FirstLogin(User user)
        {
            var service = new SessionService(HttpContext);

            var session = service.Get();
            var updateUser = _beHeardContextManager.UserRepository.GetUserByUsername(session.Username);

            updateUser.icon = user.icon;
            //settings.Preferences.Id = updateUser.Settings.Preferences.Id;

            _beHeardContextManager.SaveChanges();
            service.Save();

            return RedirectToAction("Index");
            
        }

        public IActionResult Privacy()
        {
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult UpdateDBRecords(int score, string chosen, string result) {
            var service = new SessionService(HttpContext);
            var session = service.Get();

            var newRecord = new RecordingRecord();
            newRecord.Score = score;
            newRecord.Chosen = chosen;
            newRecord.Result = result;
            var updateUser = _beHeardContextManager.UserRepository.GetUserByUsername(session.Username);

            if (updateUser.Profile.RecordingRecords == null)
            {
                var updateRecordList = new List<RecordingRecord>
                {
                    newRecord
                };
                updateUser.Profile.RecordingRecords = updateRecordList;
            }
            else 
            {
                updateUser.Profile.RecordingRecords.Add(newRecord);
            }

            _beHeardContextManager.SaveChanges();
            service.Save();

            Console.Write("gets here");
            return new EmptyResult();
        }
    }
}
