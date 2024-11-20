namespace Engage.Domain.Entities;

public class CommunicationHistoryProject : CommunicationHistory
{
    public int ProjectId { get; set; }
    public Project Project { get; set; }
}
