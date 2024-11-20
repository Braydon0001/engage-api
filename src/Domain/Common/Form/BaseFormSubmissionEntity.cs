namespace Engage.Domain.Common.Form
{
    public class BaseFormSubmissionEntity : BaseAuditableEntity
    {
        public int? EmployeeId { get; set; }
        public int? UserId { get; set; }

        public Employee Employee { get; set; }
        public User User { get; set; }
    }
}
