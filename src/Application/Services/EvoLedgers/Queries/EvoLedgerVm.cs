
using Engage.Application.Services.AnalysisPillarGroups.Queries;
using Engage.Application.Services.AnalysisPillarSubGroups.Queries;

namespace Engage.Application.Services.EvoLedgers.Queries;

public class EvoLedgerVm : IMapFrom<EvoLedger>
{
    public int Id { get; init; }
    public string LedgerCode { get; init; }
    public string Name { get; init; }
    public AnalysisPillarGroupOption AnalysisPillarGroupId { get; init; }
    public AnalysisPillarSubGroupOption AnalysisPillarSubGroupId { get; init; }
    public bool IsActive { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EvoLedger, EvoLedgerVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EvoLedgerId))
               .ForMember(d => d.AnalysisPillarSubGroupId, opt => opt.MapFrom(s => s.AnalysisPillarSubGroup))
               .ForMember(d => d.AnalysisPillarGroupId, opt => opt.MapFrom(s => s.AnalysisPillarSubGroup.AnalysisPillarGroup));
    }
}
