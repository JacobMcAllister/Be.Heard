﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Controllers
{
    public class Exercise2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
