namespace Engage.Domain.Entities.LinkEntities
{
    public class SurveyStoreFormat
    {
        public int SurveyId { get; set; }
        public int StoreFormatId { get; set; }

        // Navigation Properties
        public Survey Survey { get; set; }
        public StoreFormat StoreFormat { get; set; }
    }
}
