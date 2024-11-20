// auto-generated
namespace Engage.Domain.Entities;

public class ProductMasterColor : BaseAuditableEntity
{
    public int ProductMasterColorId { get; set; }
    public int ProductMasterId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public ProductMaster ProductMaster { get; set; }
}