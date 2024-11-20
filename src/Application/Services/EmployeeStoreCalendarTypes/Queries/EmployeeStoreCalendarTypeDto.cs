// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarTypes.Queries;

public class EmployeeStoreCalendarTypeDto : IMapFrom<EmployeeStoreCalendarType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarType, EmployeeStoreCalendarTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarTypeId));
    }
}
