namespace Engage.Domain.Entities;

public class SurveyFormExcludedStore : SurveyFormTarget
{
    public int ExcludedStoreId { get; set; }
    public Store ExcludedStore { get; set; }
}
