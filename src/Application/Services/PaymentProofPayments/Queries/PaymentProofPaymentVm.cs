
using Engage.Application.Services.Payments.Queries;
using Engage.Application.Services.PaymentProofs.Queries;

namespace Engage.Application.Services.PaymentProofPayments.Queries;

public class PaymentProofPaymentVm : IMapFrom<PaymentProofPayment>
{
    public int Id { get; init; }
    public PaymentOption PaymentId { get; init; }
    public PaymentProofOption PaymentProofId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentProofPayment, PaymentProofPaymentVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentProofPaymentId))
               .ForMember(d => d.PaymentId, opt => opt.MapFrom(s => s.Payment))
               .ForMember(d => d.PaymentProofId, opt => opt.MapFrom(s => s.PaymentProof));
    }
}
