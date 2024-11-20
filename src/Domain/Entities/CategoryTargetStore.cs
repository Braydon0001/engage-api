namespace Engage.Domain.Entities;

public class CategoryTargetStore
{
    public int CategoryTargetStoreId { get; set; }
    public int CategoryTargetId { get; set; }
    public int StoreId { get; set; }

    // Navigation Properties

    public CategoryTarget CategoryTarget { get; set; }
    public Store Store { get; set; }
}