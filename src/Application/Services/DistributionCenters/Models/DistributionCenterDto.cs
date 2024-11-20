namespace Engage.Application.Services.DistributionCenters.Models;

public class DistributionCenterDto : IMapFrom<DistributionCenter>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DistributionCenter, DistributionCenterDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.DistributionCenterId));
    }
}
