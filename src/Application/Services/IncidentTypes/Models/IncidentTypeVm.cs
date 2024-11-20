namespace Engage.Application.Services.IncidentTypes.Models;

public class IncidentTypeVm : IMapFrom<IncidentType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentType, IncidentTypeVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IncidentTypeId));
    }
}
