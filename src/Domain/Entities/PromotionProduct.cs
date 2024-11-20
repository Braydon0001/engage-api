using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class PromotionProduct : BaseAuditableEntity
    {
        public int PromotionProductId { get; set; }
        public int PromotionProductTypeId { get; set; }
        public int EngageVariantProductId { get; set; }
        public int PromotionId { get; set; }
        public string Note { get; set; }

        // Navifation Properties
        public Promotion Promotion { get; set; }  
        public PromotionProductType PromotionProductType { get; set; }
        public EngageVariantProduct EngageVariantProduct { get; set; }
    }
}
