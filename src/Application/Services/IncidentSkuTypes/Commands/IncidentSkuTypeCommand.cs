namespace Engage.Application.Services.IncidentSkuTypes.Commands;

public class IncidentSkuTypeCommand : IMapTo<IncidentSkuType>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentSkuTypeCommand, IncidentSkuType>();
    }
}
