namespace Engage.Domain.Entities.LinkEntities
{
    public class SurveyEmployee
    {
        public int SurveyId { get; set; }
        public int EmployeeId { get; set; }
        public int? TargetingId { get; set; }

        // Navigation Properties
        public Survey Survey { get; set; }
        public Employee Employee { get; set; }
        public Targeting Targeting { get; set; }
    }
}
