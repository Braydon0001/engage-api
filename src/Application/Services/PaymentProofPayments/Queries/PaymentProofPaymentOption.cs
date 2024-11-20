namespace Engage.Application.Services.PaymentProofPayments.Queries;

public class PaymentProofPaymentOption : IMapFrom<PaymentProofPayment>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentProofPayment, PaymentProofPaymentOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentProofPaymentId));
    }
}