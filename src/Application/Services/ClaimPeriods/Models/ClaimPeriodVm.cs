namespace Engage.Application.Services.ClaimPeriods.Models;

public class ClaimPeriodVm : IMapFrom<ClaimPeriod>
{
    public int Id { get; set; }
    public OptionDto ClaimYearId { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimPeriod, ClaimPeriodVm>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.ClaimPeriodId))
            .ForMember(e => e.ClaimYearId, opt => opt.MapFrom(d => new OptionDto(d.ClaimPeriodId, d.ClaimYear.Name)));
    }
}
