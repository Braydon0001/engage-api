namespace Engage.Domain.Entities.LinkEntities;

public class SupplierClaimClassification
{
    public int SupplierId { get; set; }
    public int ClaimClassificationId { get; set; }

    // Navigation Properties
    public Supplier Supplier { get; set; }
    public ClaimClassification ClaimClassification { get; set; }
}
