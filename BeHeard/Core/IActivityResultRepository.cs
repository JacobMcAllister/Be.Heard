using BeHeard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Core
{
    public interface IActivityResultRepository : IRepository<ActivityResult>
    {
        IEnumerable<ActivityResult> GetActivityResultsByUser(User user);
        IEnumerable<ActivityResult> GetFiveExerciseResults(User user, int exercise);
    }
}
