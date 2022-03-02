using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Application;

namespace BeHeard.Controllers
{
    [BeHeardAuthorize]
    public class ExercisesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Exercise1()
        {
            return View();
        }

        public IActionResult Exercise2()
        {
            return View();
        }

        public IActionResult Exercise3()
        {
            return View();
        }
    }
}
