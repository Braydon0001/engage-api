namespace Engage.Domain.Entities.LinkEntities
{
    public class SurveyEngageRegion
    {
        public int SurveyId { get; set; }
        public int EngageRegionId { get; set; }

        // Navigation Properties
        public Survey Survey { get; set; }
        public EngageRegion EngageRegion { get; set; }
    }
}
