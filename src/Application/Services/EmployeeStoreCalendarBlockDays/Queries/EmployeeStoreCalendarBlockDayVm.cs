// auto-generated
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EmployeeStoreCalendarTypes.Queries;
using Engage.Application.Services.EmployeeStoreCalendarStatuses.Queries;
using Engage.Application.Services.EmployeeStoreCalendarPeriods.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendarBlockDays.Queries;

public class EmployeeStoreCalendarBlockDayVm : IMapFrom<EmployeeStoreCalendarBlockDay>
{
    public int Id { get; set; }
    public EmployeeOption EmployeeId { get; set; }
    public EmployeeStoreCalendarTypeOption EmployeeStoreCalendarTypeId { get; set; }
    public EmployeeStoreCalendarStatusOption EmployeeStoreCalendarStatusId { get; set; }
    public DateTime CalendarDate { get; set; }
    public bool IsManagerCreated { get; set; }
    public EmployeeStoreCalendarPeriodOption EmployeeStoreCalendarPeriodId { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarBlockDay, EmployeeStoreCalendarBlockDayVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarBlockDayId))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee))
               .ForMember(d => d.EmployeeStoreCalendarTypeId, opt => opt.MapFrom(s => s.EmployeeStoreCalendarType))
               .ForMember(d => d.EmployeeStoreCalendarStatusId, opt => opt.MapFrom(s => s.EmployeeStoreCalendarStatus))
               .ForMember(d => d.EmployeeStoreCalendarPeriodId, opt => opt.MapFrom(s => s.EmployeeStoreCalendarPeriod));
    }
}
