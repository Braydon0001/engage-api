namespace Engage.Application.Services.ClaimPeriods.Models;

public class ClaimPeriodDto : IMapFrom<ClaimPeriod>
{
    public int Id { get; set; }
    public int ClaimYearId { get; set; }
    public string ClaimYearName { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimPeriod, ClaimPeriodDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.ClaimPeriodId))
            .ForMember(d => d.ClaimYearName, opt => opt.MapFrom(s => s.ClaimYear.Name));
    }
}
