
namespace Engage.Application.Services.PaymentStatuses.Queries;

public class PaymentStatusVm : IMapFrom<PaymentStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentStatus, PaymentStatusVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentStatusId));
    }
}
