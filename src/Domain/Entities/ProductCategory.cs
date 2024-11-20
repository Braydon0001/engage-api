// auto-generated
namespace Engage.Domain.Entities;

public class ProductCategory : BaseAuditableEntity
{
    public int ProductCategoryId { get; set; }
    public int ProductSubGroupId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public ProductSubGroup ProductSubGroup { get; set; }
}