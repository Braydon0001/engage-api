// auto-generated
namespace Engage.Application.Services.PayrollYears.Queries;

public class PayrollYearVm : IMapFrom<PayrollYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PayrollYear, PayrollYearVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PayrollYearId));
    }
}
