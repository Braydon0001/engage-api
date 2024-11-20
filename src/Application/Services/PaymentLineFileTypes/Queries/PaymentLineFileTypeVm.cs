
namespace Engage.Application.Services.PaymentLineFileTypes.Queries;

public class PaymentLineFileTypeVm : IMapFrom<PaymentLineFileType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLineFileType, PaymentLineFileTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentLineFileTypeId));
    }
}
