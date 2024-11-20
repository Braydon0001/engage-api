namespace Engage.Application.Services.IncidentSkuTypes.Models;

public class IncidentSkuTypeVm : IMapFrom<IncidentSkuType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentSkuType, IncidentSkuTypeVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IncidentSkuTypeId));
    }
}
