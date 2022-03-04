using BeHeard.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Services;

namespace BeHeard.Controllers
{

    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            var thisSession = HttpContext.Session.GetObjectFromJson<SessionModel>("thisSession");
            var service = new SessionService(HttpContext);
            return View(service.Get());
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

    }
}
