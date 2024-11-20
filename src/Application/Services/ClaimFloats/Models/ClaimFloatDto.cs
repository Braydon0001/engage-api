namespace Engage.Application.Services.ClaimFloats.Models;

public class ClaimFloatDto : IMapFrom<ClaimFloat>
{
    public int Id { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public decimal Amount { get; set; }
    public decimal MinimumAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? TopUpAmount { get; set; }
    public DateTime? LastToppedUp { get; set; }
    public string LastToppedUpBy { get; set; }
    public bool Disabled { get; set; }
    public List<ClaimFloatTopUpHistoryDto> ClaimFloatTopUpHistories { get; set; }
    public void Mapping(Profile profile)
    {

        profile.CreateMap<ClaimFloat, ClaimFloatDto>()
            .ForMember(d => d.Id, opts => opts.MapFrom(d => d.ClaimFloatId))
            .ForMember(d => d.RemainingAmount, opt => opt.MapFrom(d => d.Amount - (d.ClaimFloatClaims.Select(t => t.Claim.ClaimSkus.Where(s => s.Deleted == false).Select(v => v.TotalAmount).DefaultIfEmpty().Sum()).DefaultIfEmpty().Sum())))
            .ForMember(d => d.ClaimFloatTopUpHistories,
                    opt => opt.MapFrom(s => s.ClaimFloatTopUpHistory
                        .OrderByDescending(e => e.ClaimFloatTopUpHistoryId)
                        .Select(e => new ClaimFloatTopUpHistoryDto()
                        {
                            Id = e.ClaimFloatTopUpHistoryId,
                            ClaimFloatId = e.ClaimFloatId,
                            TopUpAmount = Math.Round(e.TopUpAmount, 2),
                            TopUpDate = e.Created,
                            ToppedUpBy = e.CreatedBy,
                        })
                        .ToList()));
    }
}

public class ClaimFloatTopUpHistoryDto
{
    public int Id { get; set; }
    public int ClaimFloatId { get; set; }
    public decimal TopUpAmount { get; set; }
    public DateTime TopUpDate { get; set; }
    public string ToppedUpBy { get; set; }
}
