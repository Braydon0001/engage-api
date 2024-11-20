
using Engage.Application.Services.Creditors.Queries;
using Engage.Application.Services.PaymentPeriods.Queries;
using Engage.Application.Services.PaymentStatuses.Queries;

namespace Engage.Application.Services.Payments.Queries;

public class PaymentVm : IMapFrom<Payment>
{
    public int Id { get; init; }
    public CreditorOption CreditorId { get; init; }
    public PaymentStatusOption PaymentStatusId { get; init; }
    public PaymentPeriodOption PaymentPeriodId { get; init; }
    public OptionDto EngageRegionId { get; set; }
    public string InvoiceNumber { get; init; }
    public DateTime BatchDate { get; init; }
    public ICollection<OptionDto> EngageRegionIds { get; set; }
    public DateTime InvoiceDate { get; init; }
    public bool IsClaimFromSupplier { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Payment, PaymentVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentId))
               .ForMember(d => d.CreditorId, opt => opt.MapFrom(s => s.Creditor))
               .ForMember(d => d.BatchDate, opt => opt.MapFrom(s => s.PaymentBatch.BatchDate))
               .ForMember(d => d.PaymentStatusId, opt => opt.MapFrom(s => s.PaymentStatus))
               .ForMember(d => d.PaymentPeriodId, opt => opt.MapFrom(s => s.PaymentPeriod))
               .ForMember(d => d.EngageRegionIds, opt => opt.MapFrom(s => s.PaymentBatch.BatchRegions.Select(o => new OptionDto(o.EngageRegionId, o.EngageRegion.Name))));
    }
}
