namespace Engage.Domain.Entities.LinkEntities
{
    public class EmployeeNotification
    {
        public int EmployeeId { get; set; }
        public int NotificationId { get; set; }
        
        // Navigation Properties
        public Employee Employee { get; set; }
        public Notification Notification { get; set; }
        public int Count { get; set; }
    }
}
