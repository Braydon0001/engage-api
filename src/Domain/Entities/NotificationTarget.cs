// auto-generated
namespace Engage.Domain.Entities;

public class NotificationTarget : BaseAuditableEntity
{
    public int NotificationTargetId { get; set; }
    public int NotificationId { get; set; }

    // Navigation Properties

    public Notification Notification { get; set; }
}