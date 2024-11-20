// auto-generated
using Engage.Application.Services.ProductBrands.Queries;
using Engage.Application.Services.ProductManufacturers.Queries;
using Engage.Application.Services.ProductMasterStatuses.Queries;
using Engage.Application.Services.ProductMasterSystemStatuses.Queries;
using Engage.Application.Services.ProductReasons.Queries;
using Engage.Application.Services.ProductSubCategories.Queries;
using Engage.Application.Services.ProductVendors.Queries;

namespace Engage.Application.Services.ProductMasters.Queries;

public class ProductMasterVm : IMapFrom<ProductMaster>
{
    public int Id { get; set; }
    public ProductBrandOption ProductBrandId { get; set; }
    public ProductReasonOption ProductReasonId { get; set; }
    public ProductSubCategoryOption ProductSubCategoryId { get; set; }
    public ProductMasterStatusOption ProductMasterStatusId { get; set; }
    public ProductMasterSystemStatusOption ProductMasterSystemStatusId { get; set; }
    public ProductVendorOption ProductVendorId { get; set; }
    public ProductManufacturerOption ProductManufacturerId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string LedgerCode { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMaster, ProductMasterVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterId))
               .ForMember(d => d.ProductBrandId, opt => opt.MapFrom(s => s.ProductBrand))
               .ForMember(d => d.ProductReasonId, opt => opt.MapFrom(s => s.ProductReason))
               .ForMember(d => d.ProductSubCategoryId, opt => opt.MapFrom(s => s.ProductSubCategory))
               .ForMember(d => d.ProductMasterStatusId, opt => opt.MapFrom(s => s.ProductMasterStatus))
               .ForMember(d => d.ProductMasterSystemStatusId, opt => opt.MapFrom(s => s.ProductMasterSystemStatus))
               .ForMember(d => d.ProductVendorId, opt => opt.MapFrom(s => s.ProductVendor))
               .ForMember(d => d.ProductManufacturerId, opt => opt.MapFrom(s => s.ProductManufacturer));
    }
}
