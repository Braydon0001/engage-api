// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarBlockDays.Queries;

public class EmployeeStoreCalendarBlockDayOption : IMapFrom<EmployeeStoreCalendarBlockDay>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarBlockDay, EmployeeStoreCalendarBlockDayOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreCalendarBlockDayId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Note));
    }
}