namespace Engage.Application.Services.PaymentProofPayments.Queries;

public class PaymentProofPaymentDto : IMapFrom<PaymentProofPayment>
{
    public int Id { get; init; }
    public int PaymentId { get; init; }
    public int PaymentProofId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentProofPayment, PaymentProofPaymentDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentProofPaymentId));
    }
}
