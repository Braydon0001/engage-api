// auto-generated
namespace Engage.Application.Services.InventoryWarehouses.Queries;

public class InventoryWarehouseDto : IMapFrom<InventoryWarehouse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryWarehouse, InventoryWarehouseDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryWarehouseId));
    }
}