namespace Engage.Domain.Entities;

public class SurveyFormStoreCluster : SurveyFormTarget
{
    public int StoreClusterId { get; set; }

    public StoreCluster StoreCluster { get; set; }
}
