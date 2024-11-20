// auto-generated
namespace Engage.Application.Services.ProductWarehouses.Queries;

public class ProductWarehouseOption : IMapFrom<ProductWarehouse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductWarehouse, ProductWarehouseOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductWarehouseId));
    }
}