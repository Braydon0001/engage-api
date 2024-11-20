// auto-generated
namespace Engage.Domain.Entities;

public class ProductMaster : BaseAuditableEntity
{
    public int ProductMasterId { get; set; }
    public int ProductBrandId { get; set; }
    public int ProductReasonId { get; set; }
    public int ProductSubCategoryId { get; set; }
    public int ProductMasterStatusId { get; set; }
    public int ProductMasterSystemStatusId { get; set; }
    public int ProductVendorId { get; set; }
    public int ProductManufacturerId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string LedgerCode { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public ProductBrand ProductBrand { get; set; }
    public ProductReason ProductReason { get; set; }
    public ProductSubCategory ProductSubCategory { get; set; }
    public ProductMasterStatus ProductMasterStatus { get; set; }
    public ProductMasterSystemStatus ProductMasterSystemStatus { get; set; }
    public ProductVendor ProductVendor { get; set; }
    public ProductManufacturer ProductManufacturer { get; set; }
}