using System.Collections.Generic;
using System.Threading.Tasks;
using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Services;
using BeHeard.Widgets.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeHeard.Widgets.Components;

public class PatientListViewComponent : ViewComponent
{
    private readonly BeHeardContext _context;

    public PatientListViewComponent(BeHeardContext context)
    {
        _context = context;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new PatientListModel
        {
            Patients = await DefaultAsync()
        };
        return View("Default", model);
    }

    private Task<IEnumerable<User>> DefaultAsync()
    {
        var service = new UserService(_context);
        return Task.FromResult(service.GetAllUsers());
    }
}