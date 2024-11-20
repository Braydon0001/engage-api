namespace Engage.Domain.Entities;

public class Survey : BaseAuditableEntity
{
    public Survey()
    {
        SurveyEngageRegions = new HashSet<SurveyEngageRegion>();
        SurveyEmployees = new HashSet<SurveyEmployee>();
        SurveyStores = new HashSet<SurveyStore>();
        SurveyStoreFormats = new HashSet<SurveyStoreFormat>();
        SurveyQuestions = new HashSet<SurveyQuestion>();
        SurveyInstances = new HashSet<SurveyInstance>();
    }

    public int SurveyId { get; set; }
    public int SurveyTypeId { get; set; }
    public int EngageSubGroupId { get; set; }
    public int SupplierId { get; set; }
    public int EngageBrandId { get; set; }
    public int? EngageMasterProductId { get; set; }
    public string Title { get; set; }
    public string Note { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsRecurring { get; set; }
    public bool IsEmployeeTargeting { get; set; }
    public bool IsRequired { get; set; }
    public bool IsDisabled { get; set; }

    // Navigation Properties
    public SurveyType SurveyType { get; set; }
    public EngageSubGroup EngageSubGroup { get; set; }
    public Supplier Supplier { get; set; }
    public EngageBrand EngageBrand { get; set; }
    public EngageMasterProduct EngageMasterProduct { get; set;}
    public ICollection<SurveyEmployee> SurveyEmployees { get; set; }
    public ICollection<SurveyEngageRegion> SurveyEngageRegions { get; set; }
    public ICollection<SurveyStore> SurveyStores { get; set; }
    public ICollection<SurveyStoreFormat> SurveyStoreFormats { get; set; }
    public ICollection<SurveyQuestion> SurveyQuestions { get; set; }
    public ICollection<SurveyInstance> SurveyInstances { get; set; }
}
