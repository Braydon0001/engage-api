// auto-generated
namespace Engage.Application.Services.InventoryStatuses.Queries;

public class InventoryStatusOption : IMapFrom<InventoryStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryStatus, InventoryStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryStatusId));
    }
}