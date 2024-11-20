namespace Engage.Domain.Entities;

public class ClaimFloat : BaseAuditableEntity
{
    public ClaimFloat()
    {
        ClaimFloatClaims = new HashSet<ClaimFloatClaim>();
        ClaimFloatTopUpHistory = new HashSet<ClaimFloatTopUpHistory>();
    }
    public int ClaimFloatId { get; set; }
    public int? ClaimTypeId { get; set; }
    public int EngageRegionId { get; set; }
    public int SupplierId { get; set; }
    public decimal Amount { get; set; }
    public decimal MinimumAmount { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? TopUpAmount { get; set; }
    public DateTime? LastToppedUp { get; set; }
    public string LastToppedUpBy { get; set; }

    // Navigation Properties
    public Supplier Supplier { get; set; }
    public EngageRegion EngageRegion { get; set; }
    public ClaimType ClaimType { get; set; }
    public ICollection<ClaimFloatClaim> ClaimFloatClaims { get; set; }
    public ICollection<ClaimFloatTopUpHistory> ClaimFloatTopUpHistory { get; set; }
}
