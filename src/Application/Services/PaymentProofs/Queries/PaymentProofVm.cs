
namespace Engage.Application.Services.PaymentProofs.Queries;

public class PaymentProofVm : IMapFrom<PaymentProof>
{
    public int Id { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentProof, PaymentProofVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentProofId));
    }
}
