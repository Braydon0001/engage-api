namespace Engage.Domain.Entities;

public class PaymentNotificationStatusUser
{
    public int PaymentNotificationStatusUserId { get; set; }
    public int PaymentStatusId { get; set; }
    public int EngageRegionId { get; set; }
    public int UserId { get; set; }

    public PaymentStatus PaymentStatus { get; set; }
    public EngageRegion EngageRegion { get; set; }
    public User User { get; set; }
}
