namespace Engage.Domain.Entities.LinkEntities
{
    public class NotificationRegion
    {
        public int NotificationId { get; set; }
        public int EngageRegionId { get; set; }

        public Notification Notification { get; set; }
        public EngageRegion EngageRegion { get; set; }
    }
}
