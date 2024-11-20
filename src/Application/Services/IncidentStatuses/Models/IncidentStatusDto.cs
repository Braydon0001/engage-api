namespace Engage.Application.Services.IncidentStatuses.Models;

public class IncidentStatusDto : IMapFrom<IncidentStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentStatus, IncidentStatusDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IncidentStatusId));
    }
}
