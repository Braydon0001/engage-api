namespace Engage.Application.Services.VatPeriods.Commands;

public class VatPeriodCommand : IMapTo<VatPeriod>
{
    public int VatId { get; set; }
    public DateTime StartDate { get; set; }
    public int Percent { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VatPeriodCommand, VatPeriod>();
    }
}
