namespace Engage.Application.Services.AnalysisPillarGroups.Queries;

public class AnalysisPillarGroupOption : IMapFrom<AnalysisPillarGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnalysisPillarGroup, AnalysisPillarGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AnalysisPillarGroupId));
    }
}