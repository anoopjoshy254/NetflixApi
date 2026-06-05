using System;

namespace NetflixApi.Modules.Subscription.DTOs
{
    public class SubscriptionPlanDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int MaxProfiles { get; set; }
        public string VideoQuality { get; set; }
    }

    public class UserSubscriptionDto
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public bool AutoRenew { get; set; }
    }

    public class SubscriptionStatusDto
    {
        public bool IsActive { get; set; }
        public string PlanName { get; set; }
    }
}
