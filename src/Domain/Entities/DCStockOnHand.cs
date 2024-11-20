namespace Engage.Domain.Entities;

public class DCStockOnHand : BaseAuditableEntity
{
    public int DCStockOnHandId { get; set; }
    public int DCProductId { get; set; }
    public float OnOrderQuantity { get; set; }
    public DateTime StockDate { get; set; }
    public float Value { get; set; }
    public string Note { get; set; }

    // Navigation Properties

    public DCProduct DCProduct { get; set; }
}