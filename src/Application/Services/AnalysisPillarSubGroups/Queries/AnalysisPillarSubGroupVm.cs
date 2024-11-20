
using Engage.Application.Services.AnalysisPillarGroups.Queries;

namespace Engage.Application.Services.AnalysisPillarSubGroups.Queries;

public class AnalysisPillarSubGroupVm : IMapFrom<AnalysisPillarSubGroup>
{
    public int Id { get; init; }
    public AnalysisPillarGroupOption AnalysisPillarGroupId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnalysisPillarSubGroup, AnalysisPillarSubGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AnalysisPillarSubGroupId))
               .ForMember(d => d.AnalysisPillarGroupId, opt => opt.MapFrom(s => s.AnalysisPillarGroup));
    }
}
