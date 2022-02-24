using BeHeard.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Controllers
{

    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            var thisSession = HttpContext.Session.GetObjectFromJson<SessionModel>("thisSession");

            return View(thisSession);
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
