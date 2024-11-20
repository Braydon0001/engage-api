namespace Engage.Domain.Entities;

public class CommunicationHistoryStore : CommunicationHistory
{
    public int StoreId { get; set; }
    public Store Store { get; set; }
}
