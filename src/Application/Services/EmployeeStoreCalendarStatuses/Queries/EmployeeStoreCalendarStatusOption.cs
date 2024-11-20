// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarStatuses.Queries;

public class EmployeeStoreCalendarStatusOption : IMapFrom<EmployeeStoreCalendarStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarStatus, EmployeeStoreCalendarStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarStatusId));
    }
}