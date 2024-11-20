namespace Engage.Domain.Entities;

public class CategoryStoreGroup : BaseAuditableEntity
{
    public int CategoryStoreGroupId { get; set; }
    public int CategoryGroupId { get; set; }
    public int StoreId { get; set; }

    // Navigation Properties

    public CategoryGroup CategoryGroup { get; set; }
    public Store Store { get; set; }
}