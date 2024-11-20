namespace Engage.Application.Services.PaymentYears.Queries;

public class PaymentYearOption : IMapFrom<PaymentYear>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentYear, PaymentYearOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentYearId));
    }
}