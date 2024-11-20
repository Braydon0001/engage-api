namespace Engage.Domain.Entities;

public class CreditorNotificationStatusUser
{
    public int CreditorNotificationStatusUserId { get; set; }
    public int CreditorStatusId { get; set; }
    public int? EngageRegionId { get; set; }
    public int UserId { get; set; }

    public CreditorStatus CreditorStatus { get; set; }
    public EngageRegion EngageRegion { get; set; }
    public User User { get; set; }
}
