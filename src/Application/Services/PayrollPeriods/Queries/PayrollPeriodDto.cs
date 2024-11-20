// auto-generated
namespace Engage.Application.Services.PayrollPeriods.Queries;

public class PayrollPeriodDto : IMapFrom<PayrollPeriod>
{
    public int Id { get; set; }
    public int PayrollYearId { get; set; }
    public string PayrollYearName { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PayrollPeriod, PayrollPeriodDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PayrollPeriodId));
    }
}
