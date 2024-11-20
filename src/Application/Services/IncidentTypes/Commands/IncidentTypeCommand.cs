namespace Engage.Application.Services.IncidentTypes.Commands;

public class IncidentTypeCommand : IMapTo<IncidentType>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentTypeCommand, IncidentType>();
    }
}
