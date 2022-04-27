using BeHeard.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeHeard.Controllers;

public class MedicalProviderController : Controller
{
    public IActionResult Index()
    {
        var model = new MedicalProviderViewModel();
        return View(model);
    }
}