// auto-generated
namespace Engage.Application.Services.InventoryGroups.Queries;

public class InventoryGroupVm : IMapFrom<InventoryGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryGroup, InventoryGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryGroupId));
    }
}
