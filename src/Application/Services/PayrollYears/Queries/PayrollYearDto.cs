// auto-generated
namespace Engage.Application.Services.PayrollYears.Queries;

public class PayrollYearDto : IMapFrom<PayrollYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PayrollYear, PayrollYearDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PayrollYearId));
    }
}
