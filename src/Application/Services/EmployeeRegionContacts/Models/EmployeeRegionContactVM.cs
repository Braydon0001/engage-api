namespace Engage.Application.Services.EmployeeRegionContacts.Models;

public class EmployeeRegionContactVm : IMapFrom<EmployeeRegionContact>
{
    public int Id { get; set; }
    public OptionDto EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public OptionDto EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string MobilePhone { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeRegionContact, EmployeeRegionContactVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeRegionContactId))
            .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => new OptionDto(s.EngageRegionId, s.EngageRegion.Name)))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName + " - " + s.Employee.Code)));
    }
}
