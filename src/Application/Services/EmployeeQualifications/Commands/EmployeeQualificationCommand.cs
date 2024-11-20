namespace Engage.Application.Services.EmployeeQualifications.Commands;

public class EmployeeQualificationCommand : IMapTo<EmployeeQualification>
{
    public int EducationLevelId { get; set; }
    public int InstitutionTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string InstitutionName { get; set; }
    public string FinalYearSubjects { get; set; }
    public bool IsHighestQualification { get; set; }
    public DateTime CompletedDate { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeQualificationCommand, EmployeeQualification>();
    }
}
