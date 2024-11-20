namespace Engage.Domain.Entities.LinkEntities
{
    public class EngageProductTag
    {
        public int EngageMasterProductId { get; set; }
        public int EngageTagId { get; set; }

        // Navigation Properties
        public EngageMasterProduct EngageMasterProduct { get; set; }
        public EngageTag EngageTag { get; set; }
    }
}