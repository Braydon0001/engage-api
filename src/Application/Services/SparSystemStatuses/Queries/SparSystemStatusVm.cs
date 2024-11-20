
namespace Engage.Application.Services.SparSystemStatuses.Queries;

public class SparSystemStatusVm : IMapFrom<SparSystemStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSystemStatus, SparSystemStatusVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparSystemStatusId));
    }
}
