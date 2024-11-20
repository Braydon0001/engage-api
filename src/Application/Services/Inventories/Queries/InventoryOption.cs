namespace Engage.Application.Services.Inventories.Queries;

public class InventoryOption : IMapFrom<Inventory>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Inventory, InventoryOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryId));
    }
}