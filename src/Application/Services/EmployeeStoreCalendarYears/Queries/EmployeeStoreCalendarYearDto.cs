// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarYears.Queries;

public class EmployeeStoreCalendarYearDto : IMapFrom<EmployeeStoreCalendarYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarYear, EmployeeStoreCalendarYearDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarYearId));
    }
}
