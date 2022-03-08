using BeHeard.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Controllers
{
    public class LogoutController : Controller
    {
        private readonly IAuthentication _authentication;

        public LogoutController(IAuthentication authentication)
        {
            _authentication = authentication;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Response.Cookies.Delete("token");
        return RedirectToAction("Index","Landing");
 
        }
    }
}
