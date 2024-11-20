namespace Engage.Domain.Entities;

public class SurveyFormStoreFormat : SurveyFormTarget
{
    public int StoreFormatId { get; set; }

    public StoreFormat StoreFormat { get; set; }
}
