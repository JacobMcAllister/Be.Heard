using BeHeard.Application;
using BeHeard.Application.Models;
using BeHeard.Core;
using System.Collections.Generic;
using System.Linq;

namespace BeHeard.Repositories
{
    public class ActivityResultRepository : Repository<ActivityResult>, IActivityResultRepository
    {
        public ActivityResultRepository(BeHeardContext context) : base(context)
        {
        }

        public IEnumerable<ActivityResult> GetActivityResultsByUser(User user)
        {
            return Context.UserProfiles
                .Where(x => x.User.Id == user.Id)
                .First()
                .ActivityResults;
        }


        public IEnumerable<ActivityResult> GetFiveExerciseResults(User user, int exercise)
        { 
            switch (exercise)
            {
                case 0:
                    return Context.
                        ActivityResults.
                        Where(x => x.UserId == user.Id && x.Exercise == Exercise.VolumeChasing).
                        OrderByDescending(x => x.Date).
                        Take(5);
                case 1:
                    return Context.
                        ActivityResults.
                        Where(x => x.UserId == user.Id && x.Exercise == Exercise.Breathing).
                        OrderByDescending(x => x.Date).
                        Take(5);
                case 2:
                    return Context.
                        ActivityResults.
                        Where(x => x.UserId == user.Id && x.Exercise == Exercise.Phrasing).
                        OrderByDescending(x => x.Date).
                        Take(5);
                case 3:
                    return Context.
                        ActivityResults.
                        Where(x => x.UserId == user.Id && x.Exercise == Exercise.Rote).
                        OrderByDescending(x => x.Date).
                        Take(5);
                default:
                    IEnumerable<ActivityResult> none = null;
                    System.Console.WriteLine("Error: No Exercise Input.");
                    return none;
            }
        }
    }
}
