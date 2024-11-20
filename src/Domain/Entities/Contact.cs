namespace Engage.Domain.Entities;

public class Contact : BaseAuditableEntity
{
    public Contact()
    {
        ContactItems = new HashSet<ContactItem>();
        ContactEvents = new HashSet<ContactEvent>();
    }
    public int ContactId { get; set; }
    public int StakeholderId { get; set; }
    public int? PrimaryEmailContactItemId { get; set; }
    public int? PrimaryMobileContactItemId { get; set; }
    public string FullName { get; set; }
    public string Description { get; set; }
    public string ContactType { get; set; }

    // Navigation Properties
    public Stakeholder Stakeholder { get; set; }
    public ContactItem PrimaryEmailContactItem { get; set; }
    public ContactItem PrimaryMobileContactItem { get; set; }
    public ICollection<ContactItem> ContactItems { get; private set; }
    public ICollection<ContactEvent> ContactEvents { get; private set; }
}
