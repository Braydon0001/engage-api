namespace Engage.Application.Services.Notifications.Models;

public class NotificationDto : IMapFrom<Notification>
{
    public int Id { get; set; }
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
    public ICollection<OptionDto> EngageRegions { get; set; }
    public ICollection<OptionDto> NotificationChannels { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Notification, NotificationDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.NotificationId))
            .ForMember(d => d.EngageRegions, opt => opt.MapFrom(s => s.NotificationRegions.Select(s => s.EngageRegion).Select(s => new OptionDto() { Id = s.Id, Name = s.Name })))
            .ForMember(d => d.NotificationChannels, opt => opt.MapFrom(s => s.NotificationChannels.Select(s => s.NotificationChannel).Select(s => new OptionDto() { Id = s.Id, Name = s.Name })));
    }
}
