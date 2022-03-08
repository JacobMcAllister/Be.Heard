using BeHeard.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Core;
using BeHeard.Services;

namespace BeHeard.Controllers
{

    public class SettingsController : Controller
    {
        private readonly IBeHeardContextManager _beHeardContextManager;

        public SettingsController(BeHeardContext beHeardContext)
        {
            _beHeardContextManager = new BeHeardContextManager(beHeardContext);
        }

        public IActionResult Index()
        {
            var sessionService = new SessionService(HttpContext);
            var user = _beHeardContextManager.UserRepository.GetUserByUsername(sessionService.Get().Username);
            var model = new SettingsViewModel
            {
                Address = user.Address,
                Settings = user.Settings,
                Gender = Enum.GetName(typeof(Gender), user.Gender),
            };

            return View(model);
        }
        public ActionResult Account()
        {
            return PartialView("_Account");
        }

        public ActionResult Preferences()
        {
            return PartialView("_Preferences");
        }

        public ActionResult Subscription()
        {
            return PartialView("_Subscription");
        }

        public IActionResult SaveSettings(Settings settings) {
            var service = new SessionService(HttpContext);
            var session = service.Get();
            var updateUser = _beHeardContextManager.UserRepository.GetUserByUsername(session.Username);
            updateUser.Settings = settings;
            updateUser.Settings.Preferences = settings.Preferences;

            return RedirectToAction("Index");
        }

    }
}
