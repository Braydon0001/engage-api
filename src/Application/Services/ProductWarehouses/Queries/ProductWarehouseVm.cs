// auto-generated
namespace Engage.Application.Services.ProductWarehouses.Queries;

public class ProductWarehouseVm : IMapFrom<ProductWarehouse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ProductWarehouseOption ParentId { get; set; }
    public ICollection<OptionDto> ProductWarehouseRegionIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductWarehouse, ProductWarehouseVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductWarehouseId))
               .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.Parent))
               .ForMember(d => d.ProductWarehouseRegionIds, opt => opt.MapFrom(s => s.ProductWarehouseRegions.Select(o => new OptionDto(o.EngageRegionId, o.EngageRegion.Name))));
    }
}
