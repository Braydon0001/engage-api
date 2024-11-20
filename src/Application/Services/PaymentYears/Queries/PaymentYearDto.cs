namespace Engage.Application.Services.PaymentYears.Queries;

public class PaymentYearDto : IMapFrom<PaymentYear>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentYear, PaymentYearDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentYearId));
    }
}
