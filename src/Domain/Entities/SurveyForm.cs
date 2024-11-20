namespace Engage.Domain.Entities;

public class SurveyForm : BaseFormEntity
{
    public SurveyForm()
    {
        SurveyFormProducts = new HashSet<SurveyFormProduct>();
        SurveyFormAnswers = new HashSet<SurveyFormAnswer>();
        SurveyFormQuestionGroups = new HashSet<SurveyFormQuestionGroup>();
    }
    public int SurveyFormId { get; set; }
    public int SurveyFormTypeId { get; set; }
    public int? EngageSubgroupId { get; set; }
    public int? SupplierId { get; set; }
    public int? EngageBrandId { get; set; }
    public bool IsStoreRecurring { get; set; }
    //TODO: Remove this property
    public bool IsEmployeeSurvey { get; set; }
    public bool IgnoreSubgroup { get; set; }
    public bool IsTemplate { get; init; }


    // Navigation Properties

    public SurveyFormType SurveyFormType { get; set; }
    public EngageSubGroup EngageSubGroup { get; set; }
    public Supplier Supplier { get; set; }
    public EngageBrand EngageBrand { get; set; }
    public ICollection<SurveyFormProduct> SurveyFormProducts { get; set; }
    public ICollection<SurveyFormAnswer> SurveyFormAnswers { get; set; }
    public ICollection<SurveyFormQuestionGroup> SurveyFormQuestionGroups { get; set; }
}