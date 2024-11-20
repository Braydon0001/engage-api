namespace Engage.Application.Services.Inventories.Queries;

public class InventoryDto : IMapFrom<Inventory>
{
    public int Id { get; init; }
    public int InventoryGroupId { get; init; }
    public string InventoryGroupName { get; init; }
    public int InventoryStatusId { get; init; }
    public string InventoryStatusName { get; init; }
    public int InventoryUnitTypeId { get; init; }
    public string InventoryUnitTypeName { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string BarCode { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Inventory, InventoryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryId));
    }
}
