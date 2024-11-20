namespace Engage.Domain.Entities;

public class Manufacturer: BaseAuditableEntity
{
    public int ManufacturerId { get; set; }
    public int SupplierId { get; set; }
    public int EngageRegionId { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }

    // Navigation Properties
    public EngageRegion EngageRegion { get; set; }
    public Supplier Supplier{ get; set; }
}
