namespace Engage.Application.Services.DistributionCenters.Queries;

public class DistributionCenterOption : IMapFrom<DistributionCenter>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DistributionCenter, DistributionCenterOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DistributionCenterId));
    }
}
