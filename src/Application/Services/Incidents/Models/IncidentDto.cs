namespace Engage.Application.Services.Incidents.Models;

public class IncidentDto : IMapFrom<Incident>
{
    public int Id { get; set; }
    public int ClientTypeId { get; set; }
    public string ClientTypeName { get; set; }
    public int IncidentTypeId { get; set; }
    public string IncidentTypeName { get; set; }
    public int IncidentStatusId { get; set; }
    public string IncidentStatusName { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public string IncidentNumber { get; set; }
    public DateTime IncidentDate { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Incident, IncidentDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IncidentId));
    }
}
