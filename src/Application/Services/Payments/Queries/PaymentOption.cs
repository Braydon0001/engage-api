namespace Engage.Application.Services.Payments.Queries;

public class PaymentOption : IMapFrom<Payment>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Payment, PaymentOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentId));
    }
}