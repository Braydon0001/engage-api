namespace Engage.Application.Services.EmployeePensions.Queries;

public class EmployeePensionFileDto : IMapFrom<EmployeePension>
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeePensionSchemeId { get; set; }
    public string EmployeePensionSchemeName { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeePension, EmployeePensionFileDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeePensionId))
               .ForMember(d => d.Files, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "investmentForm")))
               .ForMember(d => d.EmployeeName, opts => opts.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName + " - " + s.Employee.Code));
    }
}
