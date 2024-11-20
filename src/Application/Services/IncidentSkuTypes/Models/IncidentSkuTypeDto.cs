namespace Engage.Application.Services.IncidentSkuTypes.Models;

public class IncidentSkuTypeDto : IMapFrom<IncidentSkuType>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentSkuType, IncidentSkuTypeDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IncidentSkuTypeId));
    }
}
