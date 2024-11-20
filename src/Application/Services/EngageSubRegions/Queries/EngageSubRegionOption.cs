namespace Engage.Application.Services.EngageSubRegions.Queries;

public class EngageSubRegionOption : IMapFrom<EngageSubRegion>
{
    public int Id { get; init; }
    public int ParentId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubRegion, EngageSubRegionOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageSubRegionId))
               .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.EngageRegionId));
    }
}