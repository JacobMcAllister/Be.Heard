using System.Threading.Tasks;
using BeHeard.Widgets.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeHeard.Widgets.Components;

public class NavigationViewComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync()
    {
        var model = new NavigationModel
        {
            About                       = Url.Action("About", "Landing"),
            CreateAccount               = Url.Action("Registration", "Login"),
            Home                        = Url.Action("Index", "Landing"),
            MedicalProviderRegistration = Url.Action("Registration", "Login"), // add roles param
            SignIn                      = Url.Action("Index", "Login")
        };

        return Task.FromResult<IViewComponentResult>(View("Navbar", model));
    }
}