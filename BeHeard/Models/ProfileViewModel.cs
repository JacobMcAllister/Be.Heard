using BeHeard.Application.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Models
{
    public class ProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public string FullName => $"{UserProfile.User.FirstName} {UserProfile.User.LastName}";
        public string Gender => Enum.GetName(typeof(Gender), UserProfile.User.Gender);
        public string Subscription
        {
            get
            {
                var textInfo = new CultureInfo("en-US", false).TextInfo;
                var title = Enum.GetName(typeof(SubscriptionType), UserProfile.User.Settings.Subscription.Type);
                return textInfo.ToTitleCase(title ?? throw new InvalidOperationException());
            }
        }
    }
}
