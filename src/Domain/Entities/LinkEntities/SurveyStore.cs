namespace Engage.Domain.Entities.LinkEntities
{
    public class SurveyStore
    {
        public int SurveyId { get; set; }
        public int StoreId { get; set; }
        public int? TargetingId { get; set; }

        // Navigation Properties
        public Survey Survey { get; set; }
        public Store Store { get; set; }
        public Targeting Targeting { get; set; }
    }
}
