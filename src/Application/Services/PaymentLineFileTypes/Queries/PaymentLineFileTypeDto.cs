namespace Engage.Application.Services.PaymentLineFileTypes.Queries;

public class PaymentLineFileTypeDto : IMapFrom<PaymentLineFileType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLineFileType, PaymentLineFileTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentLineFileTypeId));
    }
}
