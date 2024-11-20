// auto-generated
using Engage.Application.Services.PayrollYears.Queries;

namespace Engage.Application.Services.PayrollPeriods.Queries;

public class PayrollPeriodVm : IMapFrom<PayrollPeriod>
{
    public int Id { get; set; }
    public PayrollYearOption PayrollYearId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PayrollPeriod, PayrollPeriodVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PayrollPeriodId))
               .ForMember(d => d.PayrollYearId, opt => opt.MapFrom(s => s.PayrollYear));
    }
}
