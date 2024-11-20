namespace Engage.Domain.Entities;

public class ProductOrderHistory : BaseAuditableEntity
{
    public int ProductOrderHistoryId { get; set; }
    public int ProductOrderId { get; set; }
    public int ProductOrderStatusId { get; set; }
    public string Reason { get; set; }

    // Navigation Properties

    public ProductOrder ProductOrder { get; set; }
    public ProductOrderStatus ProductOrderStatus { get; set; }
}