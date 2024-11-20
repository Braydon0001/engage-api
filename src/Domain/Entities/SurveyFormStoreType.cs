namespace Engage.Domain.Entities;

public class SurveyFormStoreType : SurveyFormTarget
{
    public int StoreTypeId { get; set; }

    public StoreType StoreType { get; set; }
}
