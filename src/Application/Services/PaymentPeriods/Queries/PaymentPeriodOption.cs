namespace Engage.Application.Services.PaymentPeriods.Queries;

public class PaymentPeriodOption : IMapFrom<PaymentPeriod>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentPeriod, PaymentPeriodOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentPeriodId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.Name} ({s.StartDate:MMM} {s.StartDate.Day} - {s.EndDate:MMM} {s.EndDate.Day})"));
    }
}