// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarYears.Queries;

public class EmployeeStoreCalendarYearVm : IMapFrom<EmployeeStoreCalendarYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarYear, EmployeeStoreCalendarYearVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarYearId));
    }
}
