namespace Engage.Application.Models.Configuration
{
    public class ImportFileOptions
    {
        public string SurveyStoresFolder { get; set; }
        public int SurveyStoresMaxRows { get; set; }
        public string StoreFiltersFolder { get; set; }
        public int StoreFiltersMaxRows { get; set; }
        public string ProductFiltersFolder { get; set; }
        public int ProductFiltersMaxRows { get; set; }
    }
}
