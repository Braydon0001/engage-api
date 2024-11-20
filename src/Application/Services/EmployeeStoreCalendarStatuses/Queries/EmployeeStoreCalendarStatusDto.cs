// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarStatuses.Queries;

public class EmployeeStoreCalendarStatusDto : IMapFrom<EmployeeStoreCalendarStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarStatus, EmployeeStoreCalendarStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarStatusId));
    }
}
