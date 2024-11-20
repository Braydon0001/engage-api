// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarTypes.Queries;

public class EmployeeStoreCalendarTypeOption : IMapFrom<EmployeeStoreCalendarType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarType, EmployeeStoreCalendarTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarTypeId));
    }
}