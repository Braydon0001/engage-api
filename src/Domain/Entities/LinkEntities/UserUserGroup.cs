namespace Engage.Domain.Entities.LinkEntities
{
    public class UserUserGroup : BaseAuditableEntity
    {
        public int UserUserGroupId { get; set; }
        public int UserId { get; set; }
        public int UserGroupId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public UserGroup UserGroup { get; set; }
    }
}