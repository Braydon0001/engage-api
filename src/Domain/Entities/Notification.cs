namespace Engage.Domain.Entities;

public class Notification : BaseAuditableEntity
{
    public Notification()
    {
        NotificationRegions = new HashSet<NotificationRegion>();
        EmployeeNotifications = new HashSet<EmployeeNotification>();
        NotificationEmployees = new HashSet<NotificationEmployee>();
        NotificationChannels = new HashSet<NotificationNotificationChannel>();
        NotificationEmployeeReads = new HashSet<NotificationEmployeeRead>();
    }

    public int NotificationId { get; set; }
    public int NotificationTypeId { get; set; }
    public int? NotificationCategoryId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsNational { get; set; }
    public bool Important { get; set; }
    public bool Targeted { get; set; }
    public string Subject { get; set; }
    public List<JsonFile> Files { get; set; }
    //Navigation Properties
    public NotificationType NotificationType { get; set; }
    public NotificationCategory NotificationCategory { get; set; }
    // Many to Many
    public ICollection<NotificationRegion> NotificationRegions { get; set; }
    public ICollection<EmployeeNotification> EmployeeNotifications { get; set; }
    public ICollection<NotificationEmployee> NotificationEmployees { get; set; }
    public ICollection<NotificationNotificationChannel> NotificationChannels { get; private set; }
    public ICollection<NotificationEmployeeRead> NotificationEmployeeReads { get; set; }
}
