// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarStatuses.Queries;

public class EmployeeStoreCalendarStatusVm : IMapFrom<EmployeeStoreCalendarStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarStatus, EmployeeStoreCalendarStatusVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarStatusId));
    }
}
