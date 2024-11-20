namespace Engage.Domain.Entities;

public class CommunicationHistoryOrder : CommunicationHistory
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
}
