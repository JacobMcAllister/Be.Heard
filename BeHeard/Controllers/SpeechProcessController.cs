using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Controllers
{
    public class SpeechProcessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public void StartRecording() { 

        }
    }
}
