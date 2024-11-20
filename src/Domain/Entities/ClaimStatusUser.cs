using Engage.Domain.Enums;

namespace Engage.Domain.Entities
{
    public class ClaimNotificationUser
    {
        public int ClaimNotificationUserId { get; set; }
        public int ClaimStatusId { get; set; }
        public int UserId { get; set; }
        public int EngageRegionId { get; set; }

        // Navigation Properties
        public ClaimStatus ClaimStatus { get; set; }
        public User User { get; set; }
        public EngageRegion EngageRegion { get; set; }
    }
}
