// auto-generated
namespace Engage.Application.Services.ProductWarehouses.Queries;

public class ProductWarehouseDto : IMapFrom<ProductWarehouse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public string ParentName { get; set; }
    public string EngageRegions { get; set; }
    public List<int> EngageRegionIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductWarehouse, ProductWarehouseDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductWarehouseId))
               .ForMember(d => d.ParentName, opt => opt.MapFrom(s => s.Parent.IsNotNull() ? s.Parent.Name : ""))
               .ForMember(d => d.EngageRegions, opt => opt.MapFrom(s => string.Join(", ", s.ProductWarehouseRegions.Select(x => x.EngageRegion.Name))))
               .ForMember(d => d.EngageRegionIds, opt => opt.MapFrom(s => s.ProductWarehouseRegions.Select(x => x.EngageRegionId).ToList()));
    }
}
