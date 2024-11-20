
using Engage.Application.Services.PaymentYears.Queries;

namespace Engage.Application.Services.PaymentPeriods.Queries;

public class PaymentPeriodVm : IMapFrom<PaymentPeriod>
{
    public int Id { get; init; }
    public PaymentYearOption PaymentYearId { get; init; }
    public string Name { get; init; }
    public int Number { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentPeriod, PaymentPeriodVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentPeriodId))
               .ForMember(d => d.PaymentYearId, opt => opt.MapFrom(s => s.PaymentYear));
    }
}
