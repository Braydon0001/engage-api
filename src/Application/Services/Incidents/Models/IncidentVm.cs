namespace Engage.Application.Services.Incidents.Models;

public class IncidentVm : IMapFrom<Incident>
{
    public int Id { get; set; }
    public OptionDto ClientTypeId { get; set; }
    public OptionDto IncidentTypeId { get; set; }
    public OptionDto IncidentStatusId { get; set; }
    public OptionDto SupplierId { get; set; }
    public OptionDto StoreId { get; set; }
    public string IncidentNumber { get; set; }
    public DateTime IncidentDate { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Incident, IncidentVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IncidentId))
            .ForMember(d => d.ClientTypeId, opt => opt.MapFrom(s => new OptionDto(s.ClientTypeId, s.ClientType.Name)))
            .ForMember(d => d.IncidentTypeId, opt => opt.MapFrom(s => new OptionDto(s.IncidentTypeId, s.IncidentType.Name)))
            .ForMember(d => d.IncidentStatusId, opt => opt.MapFrom(s => new OptionDto(s.IncidentStatusId, s.IncidentStatus.Name)))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => new OptionDto(s.SupplierId, s.Supplier.Name)))
            .ForMember(d => d.StoreId, opt => opt.MapFrom(s => new OptionDto(s.StoreId, s.Store.Name)));
    }
}
