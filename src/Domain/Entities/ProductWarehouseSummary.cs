// auto-generated
namespace Engage.Domain.Entities;

public class ProductWarehouseSummary : BaseAuditableEntity
{
    public int ProductWarehouseSummaryId { get; set; }
    public int ProductId { get; set; }
    public int ProductWarehouseId { get; set; }
    public int ProductPeriodId { get; set; }
    public int? EngageRegionId { get; set; }
    public float Quantity { get; set; }

    // Navigation Properties

    public Product Product { get; set; }
    public ProductWarehouse ProductWarehouse { get; set; }
    public ProductPeriod ProductPeriod { get; set; }
    public EngageRegion EngageRegion { get; set; }
}