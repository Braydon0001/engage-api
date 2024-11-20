// auto-generated
namespace Engage.Application.Services.ProductMasters.Queries;

public class ProductMasterDto : IMapFrom<ProductMaster>
{
    public int Id { get; set; }
    public int ProductBrandId { get; set; }
    public string ProductBrandName { get; set; }
    public int ProductReasonId { get; set; }
    public string ProductReasonName { get; set; }
    public int ProductSubCategoryId { get; set; }
    public string ProductSubCategoryName { get; set; }
    public int ProductMasterStatusId { get; set; }
    public string ProductMasterStatusName { get; set; }
    public int ProductMasterSystemStatusId { get; set; }
    public string ProductMasterSystemStatusName { get; set; }
    public int ProductVendorId { get; set; }
    public string ProductVendorName { get; set; }
    public int ProductManufacturerId { get; set; }
    public string ProductManufacturerName { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string LedgerCode { get; set; }
    public List<JsonFile> Files { get; set; }
    public bool Deleted { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMaster, ProductMasterDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterId))
               .ForMember(d => d.Deleted, opt => opt.MapFrom(s => s.Deleted));
    }
}
