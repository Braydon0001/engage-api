namespace Engage.Application.Services.Notifications.Models;

public class NotificationListDto : IMapFrom<Notification>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string NotificationTypeName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string Date { get; set; }
    public string EngageRegions { get; set; }
    public bool IsNational { get; set; }
    public bool Important { get; set; }
    public bool Targeted { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Notification, NotificationListDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.NotificationId))
            .ForMember(e => e.StartDate, opt => opt.MapFrom(d => DateOnly.FromDateTime(d.StartDate)))
            .ForMember(e => e.EndDate, opt => opt.MapFrom(d => DateOnly.FromDateTime(d.EndDate.Value)))
            .ForMember(e => e.Date, opt => opt.MapFrom(d => DateUtils.ShortDateString(d.StartDate, d.EndDate)))
            .ForMember(d => d.EngageRegions, opt => opt.MapFrom(d => string.Join(", ", d.NotificationRegions.Select(s => s.EngageRegion.Name))));
    }
}
