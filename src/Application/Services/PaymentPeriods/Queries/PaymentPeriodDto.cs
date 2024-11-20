namespace Engage.Application.Services.PaymentPeriods.Queries;

public class PaymentPeriodDto : IMapFrom<PaymentPeriod>
{
    public int Id { get; init; }
    public int PaymentYearId { get; init; }
    public string Name { get; init; }
    public int Number { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentPeriod, PaymentPeriodDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentPeriodId));
    }
}
