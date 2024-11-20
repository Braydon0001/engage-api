// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarBlockDays.Queries;

public class EmployeeStoreCalendarBlockDayDto : IMapFrom<EmployeeStoreCalendarBlockDay>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public int EmployeeStoreCalendarTypeId { get; set; }
    public string EmployeeStoreCalendarTypeName { get; set; }
    public int EmployeeStoreCalendarStatusId { get; set; }
    public string EmployeeStoreCalendarStatusName { get; set; }
    public string CalendarDate { get; set; }
    public bool IsManagerCreated { get; set; }
    public int EmployeeStoreCalendarPeriodId { get; set; }
    public string EmployeeStoreCalendarPeriodName { get; set; }
    public string Note { get; set; }
    public DateTime AppointmentDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarBlockDay, EmployeeStoreCalendarBlockDayDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarBlockDayId))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
               .ForMember(d => d.AppointmentDate, opt => opt.MapFrom(s => s.CalendarDate));
    }
}
