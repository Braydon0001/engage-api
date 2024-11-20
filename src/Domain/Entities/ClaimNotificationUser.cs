namespace Engage.Domain.Entities
{
    public class ClaimStatusUser
    {
        public int ClaimStatusUserId { get; set; }
        public int ClaimStatusId { get; set; }
        public int UserId { get; set; }

        // Navigation Properties
        public ClaimStatus ClaimStatus { get; set; }
        public User User { get; set; }
    }
}
