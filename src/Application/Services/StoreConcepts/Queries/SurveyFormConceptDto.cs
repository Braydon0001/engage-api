namespace Engage.Application.Services.StoreConcepts.Queries;

public class SurveyFormConceptDto : IMapFrom<SurveyForm>
{
    public int Id { get; set; }
    public int SurveyFormSubmissionId { get; set; }
    public string Title { get; set; }
    public string Note { get; set; }
    public int FirstQuestionId { get; set; }
    public string FirstQuestionAnswer { get; set; }
    public string FirstQuestionType { get; set; }
    public string FirstQuestionOptions { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public DateTime? FirstCompletionDate { get; set; }
    public int DisplayOrder { get; set; }
    public string AnswerText { get; set; }
    public string SurveyFormQuestionTypeName { get; set; }
    public bool IsParent { get; set; }
    public bool IsConcept { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<JsonRule> Rules { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyForm, SurveyFormConceptDto>()
               .ForMember(e => e.Id, opt => opt.MapFrom(s => s.SurveyFormId))
               .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.StartDate))
               .ForMember(d => d.IsParent, opt => opt.MapFrom(s => true))
               .ForMember(d => d.FirstQuestionId, opt => opt.MapFrom(s => s.SurveyFormQuestionGroups
                                                                           .OrderBy(e => e.DisplayOrder)
                                                                           .First()
                                                                           .SurveyFormQuestions
                                                                           .OrderBy(e => e.DisplayOrder)
                                                                           .First().SurveyFormQuestionId))
               .ForMember(d => d.IsConcept, opt => opt.MapFrom(s => true));
    }
}

public class StoreConceptSurveyFormAdvancedTargetingVm
{
    public bool HasAdvancedTargeting { get; set; }
    public List<int> Stores { get; set; }
    public List<int> StoreEngageRegions { get; set; }
    public List<int> StoreFormats { get; set; }
    public List<int> StoreClusters { get; set; }
    public List<int> StoreLSMs { get; set; }
    public List<int> StoreTypes { get; set; }
    public ListResult<SurveyFormConceptDto> SurveyForms { get; set; }
}