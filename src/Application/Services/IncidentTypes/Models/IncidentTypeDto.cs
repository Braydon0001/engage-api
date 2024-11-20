namespace Engage.Application.Services.IncidentTypes.Models;

public class IncidentTypeDto : IMapFrom<IncidentType>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentType, IncidentTypeDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IncidentTypeId));
    }
}
