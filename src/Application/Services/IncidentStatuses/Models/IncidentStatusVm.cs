namespace Engage.Application.Services.IncidentStatuses.Models;

public class IncidentStatusVm : IMapFrom<IncidentStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentStatus, IncidentStatusVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IncidentStatusId));
    }
}
