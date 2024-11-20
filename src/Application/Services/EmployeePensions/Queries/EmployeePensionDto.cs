// auto-generated
namespace Engage.Application.Services.EmployeePensions.Queries;

public class EmployeePensionDto : IMapFrom<EmployeePension>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeePensionSchemeId { get; set; }
    public string EmployeePensionSchemeName { get; set; }
    public int EmployeePensionCategoryId { get; set; }
    public string EmployeePensionCategoryName { get; set; }
    public int EmployeePensionContributionPercentageId { get; set; }
    public string EmployeePensionContributionPercentageName { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string LastModifiedBy { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeePension, EmployeePensionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeePensionId))
               .ForMember(d => d.EmployeeName, opts => opts.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName + " - " + s.Employee.Code));
    }
}
