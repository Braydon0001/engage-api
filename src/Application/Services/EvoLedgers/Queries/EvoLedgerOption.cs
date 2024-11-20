namespace Engage.Application.Services.EvoLedgers.Queries;

public class EvoLedgerOption : IMapFrom<EvoLedger>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EvoLedger, EvoLedgerOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EvoLedgerId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.LedgerCode));
    }
}