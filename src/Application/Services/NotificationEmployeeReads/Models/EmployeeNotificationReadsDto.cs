namespace Engage.Application.Services.NotificationEmployeeReads.Models;

public class EmployeeNotificationReadsDto : IMapFrom<NotificationEmployeeRead>
{
    public int NotificationId { get; set; }
    public string NotificationTitle { get; set; }
    public bool NotificationImportant { get; set; }
    public DateTime NotificationEndDate { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public DateTime ReadDate { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<NotificationEmployeeRead, EmployeeNotificationReadsDto>();

        profile.CreateMap<Employee, EmployeeNotificationReadsDto>()
            .ForMember(d => d.EmployeeFirstName, opts => opts.MapFrom(s => s.FirstName))
            .ForMember(d => d.EmployeeLastName, opts => opts.MapFrom(s => s.LastName));

        profile.CreateMap<Notification, EmployeeNotificationReadsDto>()
            .ForMember(d => d.NotificationTitle, opts => opts.MapFrom(s => s.Title))
            .ForMember(d => d.NotificationImportant, opts => opts.MapFrom(s => s.Important))
            .ForMember(d => d.NotificationEndDate, opts => opts.MapFrom(s => s.EndDate));
    }
}