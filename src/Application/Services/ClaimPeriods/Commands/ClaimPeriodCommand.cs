namespace Engage.Application.Services.ClaimPeriods.Commands;

public class ClaimPeriodCommand : IMapTo<ClaimPeriod>
{
    public int ClaimYearId { get; set; }
    public int ClaimId { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimPeriodCommand, ClaimPeriod>();
    }
}
