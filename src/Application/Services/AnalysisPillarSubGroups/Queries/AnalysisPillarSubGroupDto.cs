namespace Engage.Application.Services.AnalysisPillarSubGroups.Queries;

public class AnalysisPillarSubGroupDto : IMapFrom<AnalysisPillarSubGroup>
{
    public int Id { get; init; }
    public int AnalysisPillarGroupId { get; init; }
    public string AnalysisPillarGroupName { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnalysisPillarSubGroup, AnalysisPillarSubGroupDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AnalysisPillarSubGroupId));
    }
}
