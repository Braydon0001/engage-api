namespace Engage.Domain.Entities.LinkEntities;

public class StoreStoreConceptPerformance
{
    public int StoreId { get; set; }
    public int StoreConceptId { get; set; }
    public DateTime YearMonth { get; set; }
    public int FormatTarget { get; set; }
    public int StoreSkuCount { get; set; }
    public int StoreSkuPercentDist { get; set; }
    public string KpiTier { get; set; }

    //Navigation
    public Store Store { get; set; }
    public StoreConcept StoreConcept { get; set; }
}
