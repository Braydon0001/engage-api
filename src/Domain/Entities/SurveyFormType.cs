namespace Engage.Domain.Entities;

public class SurveyFormType : BaseAuditableEntity
{
    public int SurveyFormTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool HideEmployeeTargeting { get; set; }
    public bool HideEngageSupplier { get; set; }
    public bool HideEndDate { get; set; }
    public bool HideRecurring { get; set; }
    public bool HideStoreRecurring { get; set; }
    public bool HideSurveyRequired { get; set; }
    public bool HideAddQuestionGroup { get; set; }
    public bool HideAddQuestion { get; set; }
    public bool HideReorderGroup { get; set; }
    public bool HideReorderQuestion { get; set; }
    public bool HideDeleteQuestion { get; set; }
    public bool HideDisableGroup { get; set; }
    public bool HideDisableQuestion { get; set; }
    public bool UseTemplate { get; set; }
    public int? SurveyFormTemplateId { get; set; }
    public bool HideStoreTargeting { get; set; }
    public bool HideGroupRules { get; set; }
    public bool HideQuestionRules { get; set; }
}