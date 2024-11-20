namespace Engage.Application.Services.EmployeeWorkRoleContacts.Models;

public class EmployeeWorkRoleContactDto : IMapFrom<EmployeeWorkRoleContact>
{
    public int Id { get; set; }
    public int EmployeeWorkRoleId { get; set; }
    public string EmployeeWorkRoleName { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string MiddleName { get; set; }
    public string MobilePhone { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeWorkRoleContact, EmployeeWorkRoleContactDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeWorkRoleContactId))
            .ForMember(d => d.EmployeeWorkRoleName, opt => opt.MapFrom(s => s.EmployeeWorkRole.Title));
    }
}
