namespace Engage.Domain.Entities;

public class ContactItem : BaseAuditableEntity
{
    public ContactItem()
    {
        PrimaryEmailContactItems = new HashSet<Contact>();

        PrimaryMobileContactItems = new HashSet<Contact>();
    }

    public int ContactItemId { get; set; }
    public int ContactId { get; set; }
    public int ContactTypeId { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsEmergency { get; set; }
    public string Value { get; set; }

    // Navigation Properties
    public Contact Contact { get; set; }
    public ContactType ContactType { get; set; }
    public ICollection<Contact> PrimaryEmailContactItems { get; private set; }
    public ICollection<Contact> PrimaryMobileContactItems { get; private set; }
}
