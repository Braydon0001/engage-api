using Engage.Application.Services.EmployeeRegionContacts.Models;

namespace Engage.Application.Services.EmployeeContacts.Models;

public class EmployeeContactVm
{
    public List<EmployeeRegionContactDto> EmployeeRegionContacts { get; set; }
    public EmployeeManagerVm EmployeeManager { get; set; }
    public EmployeeManagerVm EmployeeLeaveManager { get; set; }
}

public class EmployeeManagerVm : IMapFrom<Employee>
{
    public int Id { get; set; }
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
        profile.CreateMap<Employee, EmployeeManagerVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.EmailAddress, opt => opt.MapFrom(s => s.EmailAddress1))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
            .ForMember(d => d.MobilePhone, opt => opt.MapFrom(s => s.WorkNumber))
            .ForMember(d => d.Title, opt => opt.MapFrom(s => string.Join(", ", s.EmployeeJobTitles.Select(s => s.EmployeeJobTitle.Name))));
    }
}

public class EmployeeWorkRoleManagerVm : IMapFrom<EmployeeWorkRole>
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
        profile.CreateMap<EmployeeWorkRole, EmployeeWorkRoleManagerVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ManagerId))
            .ForMember(d => d.EmailAddress, opt => opt.MapFrom(s => s.Manager.EmailAddress1))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Manager.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Manager.LastName))
            .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.Manager.FirstName + " " + s.Manager.LastName))
            .ForMember(d => d.MobilePhone, opt => opt.MapFrom(s => s.Manager.WorkNumber));
    }
}
