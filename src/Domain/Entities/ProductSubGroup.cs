// auto-generated
namespace Engage.Domain.Entities;

public class ProductSubGroup : BaseAuditableEntity
{
    public int ProductSubGroupId { get; set; }
    public int ProductGroupId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public ProductGroup ProductGroup { get; set; }
}