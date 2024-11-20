namespace Engage.Application.Services.PaymentLineFiles.Queries;

public class PaymentLineFileDto : IMapFrom<PaymentLineFile>
{
    public int Id { get; init; }
    public int PaymentLineId { get; init; }
    public int PaymentLineFileTypeId { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLineFile, PaymentLineFileDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentLineFileId));
    }
}
