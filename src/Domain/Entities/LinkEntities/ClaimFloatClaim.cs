namespace Engage.Domain.Entities.LinkEntities;

public class ClaimFloatClaim
{
    public int ClaimFloatId { get; set; }
    public int ClaimId { get; set; }

    // Navigation Properties 
    public ClaimFloat ClaimFloat { get; set; }
    public Claim Claim { get; set; }
}
