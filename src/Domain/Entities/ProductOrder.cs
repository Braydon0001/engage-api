namespace Engage.Domain.Entities;

public class ProductOrder : BaseAuditableEntity
{
    public int ProductOrderId { get; set; }
    public string OrderNumber { get; set; }
    public int ProductOrderStatusId { get; set; }
    public int ProductWarehouseId { get; set; }
    public int? ProductWarehouseOutId { get; set; }
    public int ProductOrderTypeId { get; set; }
    public int ProductPeriodId { get; set; }
    public int? ProductSupplierId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<JsonText> Note { get; set; }

    // Navigation Properties

    public ProductOrderStatus ProductOrderStatus { get; set; }
    public ProductWarehouse ProductWarehouse { get; set; }
    public ProductWarehouse ProductWarehouseOut { get; set; }
    public ProductOrderType ProductOrderType { get; set; }
    public ProductPeriod ProductPeriod { get; set; }
    public ProductSupplier ProductSupplier { get; set; }
}