// auto-generated
namespace Engage.Application.Services.InventoryStatuses.Queries;

public class InventoryStatusDto : IMapFrom<InventoryStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryStatus, InventoryStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryStatusId));
    }
}
