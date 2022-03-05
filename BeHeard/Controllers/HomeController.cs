using BeHeard.Application;
using BeHeard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Services;

namespace BeHeard.Controllers
{
    [BeHeardAuthorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BeHeardContextManager _beHeardContextManager;     

        public HomeController(ILogger<HomeController> logger, BeHeardContext context)
        {
            _logger = logger;
            _beHeardContextManager = new BeHeardContextManager(context);
        }

        public IActionResult Index()
        {

            var service = new SessionService(HttpContext);

            var session = service.Get();
            var model = new HomeViewModel
            {
                FirstName = session.FirstName,
                LastName = session.LastName,
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
