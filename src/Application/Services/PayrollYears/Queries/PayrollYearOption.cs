// auto-generated
namespace Engage.Application.Services.PayrollYears.Queries;

public class PayrollYearOption : IMapFrom<PayrollYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PayrollYear, PayrollYearOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PayrollYearId));
    }
}