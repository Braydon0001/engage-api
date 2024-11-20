// auto-generated
namespace Engage.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public int ProductId { get; set; }
    public int ProductMasterId { get; set; }
    public int? RelatedProductId { get; set; }
    public int ProductWarehouseId { get; set; }
    public int ProductSizeTypeId { get; set; }
    public int ProductPackSizeTypeId { get; set; }
    public int ProductModuleStatusId { get; set; }
    public int ProductSystemStatusId { get; set; }
    public int? ProductMasterColorId { get; set; }
    public int? ProductMasterSizeId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public List<JsonFile> Files { get; set; }
    public float ProductSize { get; set; }
    public float ProductPackSize { get; set; }

    // Navigation Properties

    public ProductMaster ProductMaster { get; set; }
    public Product RelatedProduct { get; set; }
    public ProductWarehouse ProductWarehouse { get; set; }
    public ProductSizeType ProductSizeType { get; set; }
    public ProductPackSizeType ProductPackSizeType { get; set; }
    public ProductModuleStatus ProductModuleStatus { get; set; }
    public ProductSystemStatus ProductSystemStatus { get; set; }
    public ProductMasterColor ProductMasterColor { get; set; }
    public ProductMasterSize ProductMasterSize { get; set; }
}