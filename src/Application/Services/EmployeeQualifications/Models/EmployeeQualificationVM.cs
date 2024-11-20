namespace Engage.Application.Services.EmployeeQualifications.Models;

public class EmployeeQualificationVm : IMapFrom<EmployeeQualification>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto EducationLevelId { get; set; }
    public OptionDto InstitutionTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string InstitutionName { get; set; }
    public string FinalYearSubjects { get; set; }
    public bool IsHighestQualification { get; set; }
    public DateTime CompletedDate { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeQualification, EmployeeQualificationVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeQualificationId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName)))
            .ForMember(d => d.EducationLevelId, opt => opt.MapFrom(s => new OptionDto(s.EducationLevelId, s.EducationLevel.Name)))
            .ForMember(d => d.InstitutionTypeId, opt => opt.MapFrom(s => new OptionDto(s.InstitutionTypeId, s.InstitutionType.Name)));
    }
}
