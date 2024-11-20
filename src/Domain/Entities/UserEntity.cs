namespace Engage.Domain.Entities;

public class UserEntity : BaseAuditableEntity
{
    public UserEntity()
    {
        UserEntityRecords = new HashSet<UserEntityRecord>();
    }

    public int UserEntityId { get; set; }
    public int UserId { get; set; }
    public string Entity { get; set; }
    public bool Deny { get; set; }

    // Navigation Properties
    public User User { get; set; }

    public ICollection<UserEntityRecord> UserEntityRecords { get; set; }
}
