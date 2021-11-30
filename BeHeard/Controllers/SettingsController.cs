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
            return View();
        }
        public ActionResult Account()
        {
            return PartialView("_Account");
        }

        public ActionResult Preferences()
        {
            return PartialView("_Preferences");
        }
    }
}
