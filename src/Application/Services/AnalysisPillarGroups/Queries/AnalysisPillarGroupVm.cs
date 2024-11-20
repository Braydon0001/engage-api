
namespace Engage.Application.Services.AnalysisPillarGroups.Queries;

public class AnalysisPillarGroupVm : IMapFrom<AnalysisPillarGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnalysisPillarGroup, AnalysisPillarGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AnalysisPillarGroupId));
    }
}
