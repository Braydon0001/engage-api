namespace Engage.Domain.Entities.LinkEntities;

public class ProductWarehouseRegion
{
    public int ProductWarehouseId { get; set; }
    public int EngageRegionId { get; set; }

    public ProductWarehouse ProductWarehouse { get; set; }
    public EngageRegion EngageRegion { get; set; }
}
