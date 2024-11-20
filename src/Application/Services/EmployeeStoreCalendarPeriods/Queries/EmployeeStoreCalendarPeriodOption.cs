// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarPeriods.Queries;

public class EmployeeStoreCalendarPeriodOption : IMapFrom<EmployeeStoreCalendarPeriod>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarPeriod, EmployeeStoreCalendarPeriodOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarPeriodId));
    }
}