namespace Engage.Domain.Entities;

public class UserEntityRecord : BaseAuditableEntity
{
    public int UserEntityRecordId { get; set; }
    public int UserEntityId { get; set; }
    public int EntityId { get; set; }

    // Navigation Properties
    public UserEntity UserEntity { get; set; }
}
