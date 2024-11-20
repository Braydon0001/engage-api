// auto-generated
namespace Engage.Application.Services.InventoryWarehouses.Queries;

public class InventoryWarehouseOption : IMapFrom<InventoryWarehouse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryWarehouse, InventoryWarehouseOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryWarehouseId));
    }
}