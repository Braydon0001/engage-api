namespace Engage.Application.Services.Employees.Models;

public class EmployeeListDto : IMapFrom<Employee>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Name { get; set; }
    public string EmailAddress1 { get; set; }
    public string JobTitleName { get; set; }
    public string EngageRegions { get; set; }
    public string EngageSubRegion { get; set; }
    public string EngageDepartments { get; set; }
    public string EmployeeJobTitles { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string TerminationReasonName { get; set; }
    public bool Disabled { get; set; }
    public bool Deleted { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeListDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
            .ForMember(d => d.JobTitleName, opt => opt.MapFrom(s => s.EmployeeJobTitle.Name))
            .ForMember(d => d.TerminationReasonName, opt => opt.MapFrom(s => (s.Disabled && s.EmployeeTerminationHistories.Count != 0) ? s.EmployeeTerminationHistories.OrderBy(t => t.EmployeeTerminationHistoryId).LastOrDefault().EmployeeTerminationReason.Name : ""))
            .ForMember(d => d.EngageRegions, opt => opt.MapFrom(s => string.Join(", ", s.EmployeeRegions.Select(s => s.EngageRegion.Name))))
            .ForMember(d => d.EngageSubRegion, opt => opt.MapFrom(s => s.EngageSubRegion.Name))
            .ForMember(d => d.EngageDepartments, opt => opt.MapFrom(s => string.Join(", ", s.EmployeeDepartments.Select(s => s.EngageDepartment.Name))))
            .ForMember(d => d.EmployeeJobTitles, opt => opt.MapFrom(s => string.Join(", ", s.EmployeeJobTitles.Select(s => s.EmployeeJobTitle.Name))));
    }
}
