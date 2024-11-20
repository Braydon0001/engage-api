// auto-generated
namespace Engage.Application.Services.Products.Queries;

public class ProductDto : IMapFrom<Product>
{
    public int Id { get; set; }
    public int ProductMasterId { get; set; }
    public string ProductMasterName { get; set; }
    public int RelatedProductId { get; set; }
    public string RelatedProductName { get; set; }
    public int ProductWarehouseId { get; set; }
    public string ProductWarehouseName { get; set; }
    public int ProductSizeTypeId { get; set; }
    public string ProductSizeTypeName { get; set; }
    public int ProductPackSizeTypeId { get; set; }
    public string ProductPackSizeTypeName { get; set; }
    public int ProductModuleStatusId { get; set; }
    public string ProductModuleStatusName { get; set; }
    public string ProductSubCategoryName { get; set; }
    public int ProductSystemStatusId { get; set; }
    public string ProductSystemStatusName { get; set; }
    public int ProductMasterColorId { get; set; }
    public string ProductMasterColorName { get; set; }
    public int? ProductMasterSizeId { get; set; }
    public string ProductMasterSizeName { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public List<JsonFile> Files { get; set; }
    public float ProductSize { get; set; }
    public float ProductPackSize { get; set; }
    public bool Disabled { get; set; }
    public bool Deleted { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductId))
               .ForMember(d => d.ProductMasterName, opt => opt.MapFrom(s => s.ProductMaster.Description))
               .ForMember(d => d.RelatedProductName, opt => opt.MapFrom(s => s.RelatedProduct.Description))
               .ForMember(d => d.ProductSubCategoryName, opt => opt.MapFrom(s => s.ProductMaster.ProductSubCategory.Name));
    }
}
