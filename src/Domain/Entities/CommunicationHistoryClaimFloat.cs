namespace Engage.Domain.Entities;

public class CommunicationHistoryClaimFloat : CommunicationHistory
{
    public int ClaimFloatId { get; set; }
    public ClaimFloat ClaimFloat { get; set; }
}
