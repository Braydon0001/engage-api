namespace Engage.Application.Services.PaymentStatuses.Queries;

public class PaymentStatusDto : IMapFrom<PaymentStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentStatus, PaymentStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentStatusId));
    }
}
