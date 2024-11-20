// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarYears.Queries;

public class EmployeeStoreCalendarYearOption : IMapFrom<EmployeeStoreCalendarYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarYear, EmployeeStoreCalendarYearOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarYearId));
    }
}