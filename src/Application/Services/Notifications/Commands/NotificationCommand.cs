namespace Engage.Application.Services.Notifications.Commands;

public class NotificationCommand : IMapTo<Notification>
{
    public int NotificationTypeId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<int> EngageRegionIds { get; set; }
    public List<int> NotificationChannelIds { get; set; }
    public bool IsNational { get; set; }
    public bool Important { get; set; }
    public bool Targeted { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<NotificationCommand, Notification>();
    }
}
