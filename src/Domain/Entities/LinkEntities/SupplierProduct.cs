namespace Engage.Domain.Entities.LinkEntities;

public class SupplierProduct
{
    public int SupplierId { get; set; }
    public int EngageMasterProductId { get; set; }

    // Navigation Properties
    public Supplier Supplier { get; set; }
    public EngageMasterProduct EngageMasterProduct { get; set; }

}
