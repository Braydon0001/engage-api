// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarGroups.Queries;

public class EmployeeStoreCalendarGroupOption : IMapFrom<EmployeeStoreCalendarGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarGroup, EmployeeStoreCalendarGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarGroupId));
    }
}