// auto-generated
namespace Engage.Application.Services.PayrollPeriods.Queries;

public class PayrollPeriodOption : IMapFrom<PayrollPeriod>
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PayrollPeriod, PayrollPeriodOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PayrollPeriodId))
               .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.PayrollYearId));
    }
}