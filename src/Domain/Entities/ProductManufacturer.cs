// auto-generated
namespace Engage.Domain.Entities;

public class ProductManufacturer : BaseAuditableEntity
{
    public int ProductManufacturerId { get; set; }
    public int ProductSupplierId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public ProductSupplier ProductSupplier { get; set; }
}