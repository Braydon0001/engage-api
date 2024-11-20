namespace Engage.Domain.Entities
{
    public class UserEngageSubGroup : BaseAuditableEntity
    {
        public int UserEngageSubGroupId { get; set; }
        public int UserId { get; set; }
        public int EngageSubGroupId { get; set; }

        //Navigation Properties
        public User User { get; set; }
        public EngageSubGroup EngageSubGroup { get; set; }
    }
}
