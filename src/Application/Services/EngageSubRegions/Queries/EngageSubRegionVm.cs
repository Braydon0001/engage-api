namespace Engage.Application.Services.EngageSubRegions.Queries;

public class EngageSubRegionVm : IMapFrom<EngageSubRegion>
{
    public int Id { get; init; }
    public OptionDto EngageRegionId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubRegion, EngageSubRegionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageSubRegionId))
               .ForMember(e => e.EngageRegionId, opt => opt.MapFrom(d => new OptionDto(d.EngageRegionId, d.EngageRegion.Name)));
    }
}
