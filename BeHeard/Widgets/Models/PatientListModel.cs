using System.Collections.Generic;
using BeHeard.Application.Models;

namespace BeHeard.Widgets.Models;

public class PatientListModel
{
    public IEnumerable<User> Patients { get; set; }
}