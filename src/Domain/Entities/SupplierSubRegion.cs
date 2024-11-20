namespace Engage.Domain.Entities;

public class SupplierSubRegion : BaseAuditableEntity
{
    public int SupplierSubRegionId { get; set; }
    public int SupplierRegionId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public SupplierRegion SupplierRegion { get; set; }
}