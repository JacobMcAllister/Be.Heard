using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Models;

namespace BeHeard.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            var model = new LoginModel
            {
                message = "hi there!"
            };
            return View(model);
        }

        public IActionResult Registration()
        {
            return View();
        }
    }
}
