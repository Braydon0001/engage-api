namespace Engage.Domain.Entities;

public class ProductOrderStatus : BaseAuditableEntity
{
    public int ProductOrderStatusId { get; set; }
    public string Name { get; set; }
}