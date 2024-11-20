namespace Engage.Domain.Entities;

public class ProductOrderLine : BaseAuditableEntity
{
    public int ProductOrderLineId { get; set; }
    public int ProductOrderId { get; set; }
    public int ProductId { get; set; }
    public int ProductOrderLineStatusId { get; set; }
    public int ProductOrderLineTypeId { get; set; }
    public decimal Amount { get; set; }
    public float Quantity { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public ProductOrder ProductOrder { get; set; }
    public Product Product { get; set; }
    public ProductOrderLineStatus ProductOrderLineStatus { get; set; }
    public ProductOrderLineType ProductOrderLineType { get; set; }
}