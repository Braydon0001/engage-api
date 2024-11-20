// auto-generated
using Engage.Application.Services.EmployeeStoreCalendarYears.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendarPeriods.Queries;

public class EmployeeStoreCalendarPeriodVm : IMapFrom<EmployeeStoreCalendarPeriod>
{
    public int Id { get; set; }
    public EmployeeStoreCalendarYearOption EmployeeStoreCalendarYearId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarPeriod, EmployeeStoreCalendarPeriodVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarPeriodId))
               .ForMember(d => d.EmployeeStoreCalendarYearId, opt => opt.MapFrom(s => s.EmployeeStoreCalendarYear));
    }
}
