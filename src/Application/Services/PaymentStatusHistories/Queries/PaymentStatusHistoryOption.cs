namespace Engage.Application.Services.PaymentStatusHistories.Queries;

public class PaymentStatusHistoryOption : IMapFrom<PaymentStatusHistory>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentStatusHistory, PaymentStatusHistoryOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentStatusHistoryId));
    }
}