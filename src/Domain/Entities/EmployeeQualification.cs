namespace Engage.Domain.Entities
{
    public class EmployeeQualification : BaseAuditableEntity
    {
        public int EmployeeQualificationId { get; set; }
        public int EmployeeId { get; set; }
        public int EducationLevelId { get; set; }
        public int InstitutionTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InstitutionName { get; set; }
        public string FinalYearSubjects { get; set; }
        public bool IsHighestQualification { get; set; }
        public DateTime CompletedDate { get; set; }
        public List<JsonFile> Files { get; set; }

        // Navigation Properties
        public Employee Employee { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public InstitutionType InstitutionType { get; set; }
    }
}
