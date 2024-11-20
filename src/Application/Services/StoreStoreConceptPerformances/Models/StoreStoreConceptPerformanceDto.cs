
namespace Engage.Application.Services.StoreStoreConceptPerformances.Models;

public class StoreStoreConceptPerformanceDto : IMapFrom<StoreStoreConceptPerformance>
{
    public int StoreId { get; set; }
    public int StoreConceptId { get; set; }
    public string StoreConceptName { get; set; }
    public int MyProperty { get; set; }
    public DateTime YearMonth { get; set; }
    public int FormatTarget { get; set; }
    public int StoreSkuCount { get; set; }
    public int StoreSkuPercentDist { get; set; }
    public string KpiTier { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreStoreConceptPerformance, StoreStoreConceptPerformanceDto>();
    }
}