namespace Engage.Domain.Entities;

public class ProductOrderType : BaseAuditableEntity
{
    public int ProductOrderTypeId { get; set; }
    public string Name { get; set; }
}