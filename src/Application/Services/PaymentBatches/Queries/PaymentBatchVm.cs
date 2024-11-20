
namespace Engage.Application.Services.PaymentBatches.Queries;

public class PaymentBatchVm : IMapFrom<PaymentBatch>
{
    public int Id { get; init; }
    public ICollection<OptionDto> EngageRegionIds { get; set; }
    public DateTime BatchDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentBatch, PaymentBatchVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentBatchId))
               .ForMember(d => d.EngageRegionIds, opt => opt.MapFrom(s => s.BatchRegions.Select(o => new OptionDto(o.EngageRegionId, o.EngageRegion.Name))));
    }
}
