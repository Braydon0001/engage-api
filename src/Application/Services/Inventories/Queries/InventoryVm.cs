// auto-generated
using Engage.Application.Services.InventoryGroups.Queries;
using Engage.Application.Services.InventoryStatuses.Queries;
using Engage.Application.Services.InventoryUnitTypes.Queries;

namespace Engage.Application.Services.Inventories.Queries;

public class InventoryVm : IMapFrom<Inventory>
{
    public int Id { get; init; }
    public InventoryGroupOption InventoryGroupId { get; init; }
    public InventoryStatusOption InventoryStatusId { get; init; }
    public InventoryUnitTypeOption InventoryUnitTypeId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string BarCode { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Inventory, InventoryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryId))
               .ForMember(d => d.InventoryGroupId, opt => opt.MapFrom(s => s.InventoryGroup))
               .ForMember(d => d.InventoryStatusId, opt => opt.MapFrom(s => s.InventoryStatus))
               .ForMember(d => d.InventoryUnitTypeId, opt => opt.MapFrom(s => s.InventoryUnitType));
    }
}
