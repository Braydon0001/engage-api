// auto-generated
namespace Engage.Application.Services.InventoryGroups.Queries;

public class InventoryGroupOption : IMapFrom<InventoryGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryGroup, InventoryGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryGroupId));
    }
}