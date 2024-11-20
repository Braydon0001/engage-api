namespace Engage.Application.Services.IncidentSkuStatuses.Models;

public class IncidentSkuStatusVm : IMapFrom<IncidentSkuStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentSkuStatus, IncidentSkuStatusVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IncidentSkuStatusId));
    }
}
