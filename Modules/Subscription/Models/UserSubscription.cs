using System;
using NetflixApi.Modules.Users.Models;

namespace NetflixApi.Modules.Subscription.Models
{
    public class UserSubscription
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int PlanId { get; set; }
        public SubscriptionPlan Plan { get; set; }
        public User User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public bool AutoRenew { get; set; }
    }
}
