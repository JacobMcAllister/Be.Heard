﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
