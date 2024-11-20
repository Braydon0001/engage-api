
namespace Engage.Application.Services.PaymentYears.Queries;

public class PaymentYearVm : IMapFrom<PaymentYear>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentYear, PaymentYearVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentYearId));
    }
}
