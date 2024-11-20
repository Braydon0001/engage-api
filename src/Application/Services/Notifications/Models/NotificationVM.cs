namespace Engage.Application.Services.Notifications.Models;

public class NotificationVm : IMapFrom<Notification>
{
    public int Id { get; set; }
    public OptionDto NotificationTypeId { get; set; }
    public OptionDto NotificationCategoryId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsNational { get; set; }
    public bool Important { get; set; }
    public bool Targeted { get; set; }
    public string Subject { get; set; }
    public List<OptionDto> EngageRegionIds { get; set; }
    public List<OptionDto> NotificationChannelIds { get; set; }
    public List<JsonFile> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Notification, NotificationVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.NotificationId))
            .ForMember(d => d.NotificationTypeId, opt => opt.MapFrom(s => new OptionDto(s.NotificationTypeId, s.NotificationType.Name)))
            .ForMember(d => d.NotificationCategoryId, opt => opt.MapFrom(s => s.NotificationCategoryId.HasValue ? new OptionDto(s.NotificationCategoryId.Value, s.NotificationCategory.Name) : null))
            .ForMember(d => d.EngageRegionIds, opt => opt.MapFrom(s => s.NotificationRegions.Select(o => new OptionDto(o.EngageRegionId, o.EngageRegion.Name))))
            .ForMember(d => d.NotificationChannelIds, opt => opt.MapFrom(s => s.NotificationChannels.Select(o => new OptionDto(o.NotificationChannelId, o.NotificationChannel.Name))));
    }
}
