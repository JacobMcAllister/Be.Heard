using System.Collections.Generic;
using BeHeard.Application.Models;

namespace BeHeard.Widgets.Models;

public class PatientDetailModel
{
    public User User { get; set; }
    public ICollection<ActivityResult> ActivityResults { get; set; }
}