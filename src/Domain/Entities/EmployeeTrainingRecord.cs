namespace Engage.Domain.Entities
{
    public class EmployeeTrainingRecord : BaseAuditableEntity
    {
        public int EmployeeTrainingRecordId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeTrainingStatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CourseName { get; set; }
        public string CertificateNo { get; set; }
        public DateTime? CertificateExpiryDate { get; set; }
        public bool IsAddReminder { get; set; }
        public string CourseResult { get; set; }
        public decimal DirectCourseCost { get; set; }
        public string InvoiceNo { get; set; }
        public decimal EmployeeRate { get; set; }
        public string Facilitator { get; set; }
        public decimal TravelCost { get; set; }
        public string Assessor { get; set; }
        public decimal AccommodationCost { get; set; }
        public decimal FacilitatorCost { get; set; }
        public decimal FoodAndBeverageCost { get; set; }
        public decimal Additional5Cost { get; set; }
        public decimal Additional6Cost { get; set; }
        public string Note { get; set; }

        public Employee Employee { get; set; }
        public EmployeeTrainingStatus EmployeeTrainingStatus { get; set; }
    }
}
