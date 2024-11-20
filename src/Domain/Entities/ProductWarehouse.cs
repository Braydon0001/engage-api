namespace Engage.Domain.Entities;

public class ProductWarehouse : BaseAuditableEntity
{
    public ProductWarehouse()
    {
        ProductWarehouseRegions = new HashSet<ProductWarehouseRegion>();
    }

    public int ProductWarehouseId { get; set; }
    public int? EngageRegionId { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public EngageRegion EngageRegion { get; set; }
    public ProductWarehouse Parent { get; set; }

    // Many to Many

    public ICollection<ProductWarehouseRegion> ProductWarehouseRegions { get; set; }
}