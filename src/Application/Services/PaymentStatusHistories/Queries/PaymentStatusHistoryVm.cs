
using Engage.Application.Services.Payments.Queries;
using Engage.Application.Services.PaymentStatuses.Queries;

namespace Engage.Application.Services.PaymentStatusHistories.Queries;

public class PaymentStatusHistoryVm : IMapFrom<PaymentStatusHistory>
{
    public int Id { get; init; }
    public PaymentOption PaymentId { get; init; }
    public PaymentStatusOption PaymentStatusId { get; init; }
    public string Reason { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentStatusHistory, PaymentStatusHistoryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentStatusHistoryId))
               .ForMember(d => d.PaymentId, opt => opt.MapFrom(s => s.Payment))
               .ForMember(d => d.PaymentStatusId, opt => opt.MapFrom(s => s.PaymentStatus));
    }
}
