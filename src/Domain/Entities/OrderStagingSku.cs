namespace Engage.Domain.Entities;

public class OrderStagingSku : BaseAuditableEntity
{
    public int OrderStagingSkuId { get; set; }
    public int OrderStagingId { get; set; }
    public string ProductName { get; set; }
    public string Barcode { get; set; }
    public string UnitType { get; set; }
    public int Quantity { get; set; }

    // Navigation Properties

    public OrderStaging OrderStaging { get; set; }
}