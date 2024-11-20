namespace Engage.Domain.Entities
{
    public class EmployeeSkillsDevelopment : BaseAuditableEntity
    {
        public int EmployeeSkillsDevelopmentId { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public ProgressStatus Status { get; set; }
        public float Cost { get; set; }

        // Navigation Propterties
        public Employee Employee { get; set; }
    }
}
