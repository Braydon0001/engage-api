namespace Engage.Domain.Entities
{
    public class EmployeeSuspension : BaseAuditableEntity
    {
        public int EmployeeSuspensionId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeSuspensionReasonId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<JsonFile> Files { get; set; }

        // Navigation Properties
        public Employee Employee { get; set; }
        public EmployeeSuspensionReason EmployeeSuspensionReason { get; set; }
    }
}
