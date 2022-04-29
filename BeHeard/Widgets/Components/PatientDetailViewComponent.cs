using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Services;
using BeHeard.Widgets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BeHeard.Widgets.Components;

public class PatientDetailViewComponent : ViewComponent
{
    private readonly BeHeardContext _context;
    private readonly BeHeardContextManager _contextManager;

    public PatientDetailViewComponent(BeHeardContext context)
    {
        _context = context;
        _contextManager = new BeHeardContextManager(context);
    }
    
    public Task<IViewComponentResult> InvokeAsync(Guid userId)
    {
        var user = _contextManager.UserRepository.Get(userId);
        var activityResults = 
            _contextManager.ActivityResultRepository.GetActivityResultsByUser(user) ?? new Collection<ActivityResult>();
        
        var model = new PatientDetailModel
        {
            ActivityResults = activityResults.ToList(),
            User = user
        };
            
        return Task.FromResult<IViewComponentResult>(View("Default", model));
    }
}