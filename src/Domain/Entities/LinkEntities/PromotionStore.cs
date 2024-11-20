namespace Engage.Domain.Entities.LinkEntities
{
    public class PromotionStore
    {
        public int PromotionId { get; set; }
        public int StoreId { get; set; }
        public int? TargetingId { get; set; }

        // Navigation Properties
        public Promotion Promotion { get; set; }
        public Store Store { get; set; }
        public Targeting Targeting { get; set; }
    }
}
