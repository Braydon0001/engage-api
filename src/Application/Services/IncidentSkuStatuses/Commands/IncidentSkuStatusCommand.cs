namespace Engage.Application.Services.IncidentSkuStatuses.Commands;

public class IncidentSkuStatusCommand : IMapTo<IncidentSkuStatus>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentSkuStatusCommand, IncidentSkuStatus>();
    }
}
