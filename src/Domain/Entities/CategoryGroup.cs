namespace Engage.Domain.Entities;

public class CategoryGroup : BaseAuditableEntity
{
    public int CategoryGroupId { get; set; }
    public string Name { get; set; }
}