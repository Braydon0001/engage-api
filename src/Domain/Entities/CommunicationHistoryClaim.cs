namespace Engage.Domain.Entities;

public class CommunicationHistoryClaim : CommunicationHistory
{
    public int ClaimId { get; set; }
    public Claim Claim { get; set; }
}
