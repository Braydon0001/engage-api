namespace Engage.Domain.Entities;

public class ClaimFloatTopUpHistory : BaseAuditableEntity
{
    public int ClaimFloatTopUpHistoryId { get; set; }
    public int ClaimFloatId { get; set; }
    public decimal TopUpAmount { get; set; }

    // Navigation Properties
    public ClaimFloat ClaimFloat { get; set; }
}
