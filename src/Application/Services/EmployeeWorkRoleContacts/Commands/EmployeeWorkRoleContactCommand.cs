namespace Engage.Application.Services.EmployeeWorkRoleContacts.Commands;

public class EmployeeWorkRoleContactCommand : IMapTo<EmployeeWorkRoleContact>
{
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
        profile.CreateMap<EmployeeWorkRoleContactCommand, EmployeeWorkRoleContact>();
    }
}
