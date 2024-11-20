namespace Engage.Application.Services.IncidentSkus.Commands;

public class IncidentSkuCommand : IMapTo<IncidentSku>
{
    public int IncidentId { get; set; }
    public int IncidentSkuTypeId { get; set; }
    public int IncidentSkuStatusId { get; set; }
    public int DCProductId { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentSkuCommand, IncidentSku>();
    }
}
