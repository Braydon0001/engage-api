namespace Engage.Application.Services.EmployeePopiConsents.Models;

public class EmployeePopiConsentDto : IMapFrom<EmployeePopiConsent>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public string EmployeeEmailAddress1 { get; set; }
    public IEnumerable<OptionDto> EmployeeRegions { get; set; }
    public DateTime DateOfConsent { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeePopiConsent, EmployeePopiConsentDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeePopiConsentId))
            .ForMember(d => d.EmployeeRegions, opt => opt.MapFrom(s => s.Employee.EmployeeRegions.Select(e => new OptionDto(e.EngageRegionId, e.EngageRegion.Name))));
    }
}
