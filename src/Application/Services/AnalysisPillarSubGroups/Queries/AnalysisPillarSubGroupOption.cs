namespace Engage.Application.Services.AnalysisPillarSubGroups.Queries;

public class AnalysisPillarSubGroupOption : IMapFrom<AnalysisPillarSubGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnalysisPillarSubGroup, AnalysisPillarSubGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AnalysisPillarSubGroupId));
    }
}