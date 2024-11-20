namespace Engage.Application.Services.PaymentLineFiles.Queries;

public class PaymentLineFileOption : IMapFrom<PaymentLineFile>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLineFile, PaymentLineFileOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentLineFileId));
    }
}