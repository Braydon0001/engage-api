namespace Engage.Domain.Entities
{
    public class ClaimHistoryHeader : BaseAuditableEntity
    {
        public int ClaimHistoryHeaderId { get; set; }
        public int? ClaimStatusId { get; set; }
        public int? ClaimSupplierStatusId { get; set; }
        public int ClaimClassificationId { get; set; }
        public int EngageRegionId { get; set; }

        // Navigation Properties
        public ClaimStatus ClaimStatus { get; set; }
        public ClaimSupplierStatus ClaimSupplierStatus { get; set; }
        public ClaimClassification ClaimClassification { get; set; }
        public EngageRegion EngageRegion { get; set; }
    }
}