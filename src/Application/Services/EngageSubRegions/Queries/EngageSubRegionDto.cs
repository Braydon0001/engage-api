namespace Engage.Application.Services.EngageSubRegions.Queries;

public class EngageSubRegionDto : IMapFrom<EngageSubRegion>
{
    public int Id { get; init; }
    public int EngageRegionId { get; init; }
    public string EngageRegionName { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubRegion, EngageSubRegionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageSubRegionId));
    }
}
