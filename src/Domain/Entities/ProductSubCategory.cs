// auto-generated
namespace Engage.Domain.Entities;

public class ProductSubCategory : BaseAuditableEntity
{
    public int ProductSubCategoryId { get; set; }
    public int ProductCategoryId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public ProductCategory ProductCategory { get; set; }
}