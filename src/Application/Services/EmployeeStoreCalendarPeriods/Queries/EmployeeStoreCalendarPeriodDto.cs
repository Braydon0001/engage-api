// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarPeriods.Queries;

public class EmployeeStoreCalendarPeriodDto : IMapFrom<EmployeeStoreCalendarPeriod>
{
    public int Id { get; set; }
    public int EmployeeStoreCalendarYearId { get; set; }
    public string EmployeeStoreCalendarYearName { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarPeriod, EmployeeStoreCalendarPeriodDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarPeriodId));
    }
}
