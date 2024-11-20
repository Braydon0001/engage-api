namespace Engage.Application.Services.PaymentLines.Queries;

public class PaymentLineOption : IMapFrom<PaymentLine>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLine, PaymentLineOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentLineId));
    }
}