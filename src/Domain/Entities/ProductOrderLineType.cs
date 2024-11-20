namespace Engage.Domain.Entities;

public class ProductOrderLineType : BaseAuditableEntity
{
    public int ProductOrderLineTypeId { get; set; }
    public string Name { get; set; }
}