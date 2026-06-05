namespace NetflixApi.Modules.Subscription.Models
{
    public class SubscriptionPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int MaxProfiles { get; set; }
        public string VideoQuality { get; set; }
        public bool IsActive { get; set; }
    }
}
