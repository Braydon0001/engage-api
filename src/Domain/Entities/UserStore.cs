namespace Engage.Domain.Entities
{
    public class UserStore : BaseAuditableEntity
    {
        public int UserStoreId { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }

        //Navigation Properties
        public User User { get; set; }
        public Store Store { get; set; }
    }
}
