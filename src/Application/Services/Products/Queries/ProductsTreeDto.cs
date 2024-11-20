namespace Engage.Application.Services.Products.Queries;

public class ProductsTreeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsParent { get; set; }
    public bool Disabled { get; set; }
    public bool Deleted { get; set; }
    public string Description { get; set; }
    public string ProductSubCategoryName { get; set; }
    public string ProductBrandName { get; set; }
    public string ProductVendorName { get; set; }
    public string ProductManufacturerName { get; set; }
    public string Code { get; set; }
    public string ProductMasterStatusName { get; set; }
    public string ProductMasterSystemStatusName { get; set; }
    public string ProductReasonName { get; set; }
    public List<JsonFile> Files { get; set; }
}
