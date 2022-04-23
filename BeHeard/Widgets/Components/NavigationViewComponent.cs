using System.Threading.Tasks;
using BeHeard.Widgets.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeHeard.Widgets.Components;

public class NavigationViewComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(string navType = "default")
    {
        if (navType == "side")
        {
            var model = new SideNavigationModel
            {
                Admin               = null,
                AdminIcon           = "fas fa-user-lock",
                Dashboard           = Url.Action("Index", "Dashboard"),
                DashboardIcon       = "fas fa-th-large",
                Exercises           = Url.Action("Index", "Exercises"),
                ExercisesIcon       = "fas fa-dumbbell",
                Home                = Url.Action("Index", "Home"),
                HomeIcon            = "fas fa-house-user",
                MedicalProvider     = null,
                MedicalProviderIcon = "fas fa-user-md"
            };
            return Task.FromResult<IViewComponentResult>(View("SideNav", model));
        }
        else
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
}