// auto-generated
using Engage.Application.Services.ProductMasterColors.Queries;
using Engage.Application.Services.ProductMasters.Queries;
using Engage.Application.Services.ProductMasterSizes.Queries;
using Engage.Application.Services.ProductModuleStatuses.Queries;
using Engage.Application.Services.ProductPackSizeTypes.Queries;
using Engage.Application.Services.ProductSizeTypes.Queries;
using Engage.Application.Services.ProductSystemStatuses.Queries;
using Engage.Application.Services.ProductWarehouses.Queries;

namespace Engage.Application.Services.Products.Queries;

public class ProductVm : IMapFrom<Product>
{
    public int Id { get; set; }
    public ProductMasterOption ProductMasterId { get; set; }
    public ProductOption RelatedProductId { get; set; }
    public ProductWarehouseOption ProductWarehouseId { get; set; }
    public ProductSizeTypeOption ProductSizeTypeId { get; set; }
    public ProductPackSizeTypeOption ProductPackSizeTypeId { get; set; }
    public ProductModuleStatusOption ProductModuleStatusId { get; set; }
    public ProductSystemStatusOption ProductSystemStatusId { get; set; }
    public ProductMasterColorOption ProductMasterColorId { get; set; }
    public ProductMasterSizeOption ProductMasterSizeId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public List<JsonFile> Files { get; set; }
    public float ProductSize { get; set; }
    public float ProductPackSize { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductId))
               .ForMember(d => d.ProductMasterId, opt => opt.MapFrom(s => s.ProductMaster))
               .ForMember(d => d.RelatedProductId, opt => opt.MapFrom(s => s.RelatedProduct))
               .ForMember(d => d.ProductWarehouseId, opt => opt.MapFrom(s => s.ProductWarehouse))
               .ForMember(d => d.ProductSizeTypeId, opt => opt.MapFrom(s => s.ProductSizeType))
               .ForMember(d => d.ProductPackSizeTypeId, opt => opt.MapFrom(s => s.ProductPackSizeType))
               .ForMember(d => d.ProductModuleStatusId, opt => opt.MapFrom(s => s.ProductModuleStatus))
               .ForMember(d => d.ProductSystemStatusId, opt => opt.MapFrom(s => s.ProductSystemStatus))
               .ForMember(d => d.ProductMasterColorId, opt => opt.MapFrom(s => s.ProductMasterColor))
               .ForMember(d => d.ProductMasterSizeId, opt => opt.MapFrom(s => s.ProductMasterSize));
    }
}
