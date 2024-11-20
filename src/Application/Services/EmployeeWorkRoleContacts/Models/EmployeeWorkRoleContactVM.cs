namespace Engage.Application.Services.EmployeeWorkRoleContacts.Models;

public class EmployeeWorkRoleContactVm : IMapFrom<EmployeeWorkRoleContact>
{
    public int Id { get; set; }
    public OptionDto EmployeeWorkRoleId { get; set; }
    public string EmployeeWorkRoleName { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string MiddleName { get; set; }
    public string MobilePhone { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeWorkRoleContact, EmployeeWorkRoleContactVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeWorkRoleContactId))
            .ForMember(d => d.EmployeeWorkRoleId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeWorkRoleId, s.EmployeeWorkRole.Title)));
    }
}
