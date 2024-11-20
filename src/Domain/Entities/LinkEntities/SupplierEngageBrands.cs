namespace Engage.Domain.Entities.LinkEntities;

public class SupplierEngageBrand
{
    public int SupplierId { get; set; }
    public int EngageBrandId { get; set; }

    // Navigation Properties
    public Supplier Supplier { get; set; }
    public EngageBrand EngageBrand { get; set; }
}
