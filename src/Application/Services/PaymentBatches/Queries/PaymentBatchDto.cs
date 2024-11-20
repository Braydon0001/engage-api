namespace Engage.Application.Services.PaymentBatches.Queries;

public class PaymentBatchDto : IMapFrom<PaymentBatch>
{
    public int Id { get; init; }
    public DateTime BatchDate { get; init; }
    public string EngageRegions { get; init; }
    public DateTime Created { get; init; }
    public string CreatedBy { get; init; }
    public int NumberOfPayments { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentBatch, PaymentBatchDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentBatchId))
               .ForMember(d => d.EngageRegions, opt => opt.MapFrom(s => string.Join(", ", s.BatchRegions.Select(s => s.EngageRegion.Name))))
               .ForMember(d => d.NumberOfPayments, opt => opt.MapFrom(s => s.Payments.Count));
    }
}
