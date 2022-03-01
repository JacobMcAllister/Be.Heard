using System;

namespace BeHeard.Application.Models
{
    public class Subscription
    {
        public Guid             Id   { get; set; }
        public SubscriptionType Type { get; set; }
    }

    public enum SubscriptionType
    {
        Free,
        Paid,
        PaidWithCareTeam,
    }
}
