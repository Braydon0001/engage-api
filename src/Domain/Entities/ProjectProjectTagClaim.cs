namespace Engage.Domain.Entities;

public class ProjectProjectTagClaim : ProjectProjectTag
{
    public int ClaimId { get; set; }
    public Claim Claim { get; set; }
}
