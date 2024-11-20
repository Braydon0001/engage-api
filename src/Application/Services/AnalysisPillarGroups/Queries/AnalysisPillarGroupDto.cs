namespace Engage.Application.Services.AnalysisPillarGroups.Queries;

public class AnalysisPillarGroupDto : IMapFrom<AnalysisPillarGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnalysisPillarGroup, AnalysisPillarGroupDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AnalysisPillarGroupId));
    }
}
