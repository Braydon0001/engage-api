namespace Engage.Application.Services.IncidentStatuses.Commands;

public class IncidentStatusCommand : IMapTo<IncidentStatus>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentStatusCommand, IncidentStatus>();
    }
}
