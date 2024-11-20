namespace Engage.Application.Services.PaymentProofs.Queries;

public class PaymentProofOption : IMapFrom<PaymentProof>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentProof, PaymentProofOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentProofId));
    }
}