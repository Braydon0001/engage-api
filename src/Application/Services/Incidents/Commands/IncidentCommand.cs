namespace Engage.Application.Services.Incidents.Commands;

public class IncidentCommand : IMapTo<Incident>
{
    public int ClientTypeId { get; set; }
    public int IncidentTypeId { get; set; }
    public int IncidentStatusId { get; set; }
    public int SupplierId { get; set; }
    public int StoreId { get; set; }
    public string IncidentNumber { get; set; }
    public DateTime IncidentDate { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentCommand, Incident>();
    }
}
