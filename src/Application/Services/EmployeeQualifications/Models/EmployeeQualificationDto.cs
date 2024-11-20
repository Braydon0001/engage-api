namespace Engage.Application.Services.EmployeeQualifications.Models;

public class EmployeeQualificationDto : IMapFrom<EmployeeQualification>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int EducationLevelId { get; set; }
    public string EducationLevelName { get; set; }
    public int InstitutionTypeId { get; set; }
    public string InstitutionTypeName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string InstitutionName { get; set; }
    public string FinalYearSubjects { get; set; }
    public bool IsHighestQualification { get; set; }
    public DateTime CompletedDate { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeQualification, EmployeeQualificationDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeQualificationId))
            .ForMember(d => d.EducationLevelName, opt => opt.MapFrom(s => s.EducationLevel.Name))
            .ForMember(d => d.InstitutionTypeName, opt => opt.MapFrom(s => s.InstitutionType.Name));
    }
}
