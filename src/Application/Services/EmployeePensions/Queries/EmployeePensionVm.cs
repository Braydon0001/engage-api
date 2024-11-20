using Engage.Application.Services.Employees.Queries;

namespace Engage.Application.Services.EmployeePensions.Queries;

public class EmployeePensionVm : IMapFrom<EmployeePension>
{
    public int Id { get; set; }
    public EmployeeOption EmployeeId { get; set; }
    public OptionDto EmployeePensionSchemeId { get; set; }
    public OptionDto EmployeePensionCategoryId { get; set; }
    public OptionDto EmployeePensionContributionPercentageId { get; set; }
    public List<JsonFile> FileInvestmentForm { get; set; }
    public List<JsonFile> FileDeathNominationForm { get; set; }
    public List<JsonFile> FileFuneralCoverForm { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeePension, EmployeePensionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeePensionId))
               .ForMember(d => d.FileInvestmentForm, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "investmentForm")))
               .ForMember(d => d.FileDeathNominationForm, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "deathNominationForm")))
               .ForMember(d => d.FileFuneralCoverForm, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "funeralCoverForm")))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee))
               .ForMember(d => d.EmployeePensionSchemeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeePensionSchemeId, s.EmployeePensionScheme.Name)))
               .ForMember(d => d.EmployeePensionCategoryId, opt => opt.MapFrom(s => new OptionDto(s.EmployeePensionCategoryId, s.EmployeePensionCategory.Name)))
               .ForMember(d => d.EmployeePensionContributionPercentageId, opt => opt.MapFrom(s => new OptionDto(s.EmployeePensionContributionPercentageId, s.EmployeePensionContributionPercentage.Name)));
    }
}
