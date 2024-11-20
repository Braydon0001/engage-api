
using Engage.Application.Services.PaymentLines.Queries;
using Engage.Application.Services.PaymentLineFileTypes.Queries;

namespace Engage.Application.Services.PaymentLineFiles.Queries;

public class PaymentLineFileVm : IMapFrom<PaymentLineFile>
{
    public int Id { get; init; }
    public PaymentLineOption PaymentLineId { get; init; }
    public PaymentLineFileTypeOption PaymentLineFileTypeId { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLineFile, PaymentLineFileVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentLineFileId))
               .ForMember(d => d.PaymentLineId, opt => opt.MapFrom(s => s.PaymentLine))
               .ForMember(d => d.PaymentLineFileTypeId, opt => opt.MapFrom(s => s.PaymentLineFileType));
    }
}
