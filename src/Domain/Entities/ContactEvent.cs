namespace Engage.Domain.Entities;

public class ContactEvent : BaseAuditableEntity
{
    public int ContactEventId { get; set; }
    public int ContactId { get; set; }
    public int EventTypeId { get; set; }
    public int FrequencyId { get; set; }

    public DateTime EventDate { get; set; }
    public bool IsRecurringEvent { get; set; }
    public string Note { get; set; }

    // Navigation Properties
    public Contact Contact { get; set; }
    public EventType EventType { get; set; }
    public FrequencyType Frequency { get; set; }
}
