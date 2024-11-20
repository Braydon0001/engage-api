namespace Engage.Application.Services.PaymentStatusHistories.Queries;

public class PaymentStatusHistoryDto : IMapFrom<PaymentStatusHistory>
{
    public int Id { get; init; }
    public int PaymentId { get; init; }
    public int PaymentStatusId { get; init; }
    public string Reason { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentStatusHistory, PaymentStatusHistoryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentStatusHistoryId));
    }
}
