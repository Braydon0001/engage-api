namespace Engage.Application.Services.EvoLedgers.Queries;

public class EvoLedgerDto : IMapFrom<EvoLedger>
{
    public int Id { get; init; }
    public string LedgerCode { get; init; }
    public string Name { get; init; }
    public int AnalysisPillarSubGroupId { get; init; }
    public string AnalysisPillarSubGroupName { get; init; }
    public bool IsActive { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EvoLedger, EvoLedgerDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EvoLedgerId));
    }
}
