namespace Engage.Domain.Entities;

public class ProductOrderLineStatus : BaseAuditableEntity
{
    public int ProductOrderLineStatusId { get; set; }
    public string Name { get; set; }
}