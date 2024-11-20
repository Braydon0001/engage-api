namespace Engage.Domain.Entities.LinkEntities;

public class NotificationEmployeeRead
{
    public int NotificationId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime ReadDate { get; set; }

    //Navigation Properties
    public Notification Notification { get; set; }
    public Employee Employee { get; set; }


}
