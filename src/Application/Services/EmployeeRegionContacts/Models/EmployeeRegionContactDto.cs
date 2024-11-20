namespace Engage.Application.Services.EmployeeRegionContacts.Models;

public class EmployeeRegionContactDto : IMapFrom<EmployeeRegionContact>
{
    public int Id { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MobilePhone { get; set; }
    public string Title { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeRegionContact, EmployeeRegionContactDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeRegionContactId))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.EngageRegion.Name))
            .ForMember(d => d.EmailAddress, opt => opt.MapFrom(s => s.Employee.EmailAddress1))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Employee.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Employee.LastName))
            .ForMember(d => d.Title, opt => opt.MapFrom(s => string.Join(", ", s.Employee.EmployeeJobTitles.Select(s => s.EmployeeJobTitle.Name))));
    }
}
