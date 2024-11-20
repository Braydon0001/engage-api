namespace Engage.Domain.Entities;

public class SurveyFormStore : SurveyFormTarget
{
    public int StoreId { get; set; }
    public Store Store { get; set; }
}
