namespace Engage.Application.Services.SurveyFormTypes.Queries;

public class SurveyFormTypeDto : IMapFrom<SurveyFormType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public bool HideEmployeeTargeting { get; init; }
    public bool HideEngageSupplier { get; init; }
    public bool HideEndDate { get; init; }
    public bool HideRecurring { get; set; }
    public bool HideStoreRecurring { get; set; }
    public bool HideSurveyRequired { get; set; }
    public bool HideAddQuestionGroup { get; set; }
    public bool HideAddQuestion { get; set; }
    public bool HideDeleteQuestion { get; set; }
    public bool HideDisableGroup { get; set; }
    public bool HideDisableQuestion { get; set; }
    public bool HideReorderGroup { get; set; }
    public bool HideReorderQuestion { get; set; }
    public bool UseTemplate { get; set; }
    public int? SurveyFormTemplateId { get; set; }
    public bool HideStoreTargeting { get; set; }
    public bool HideGroupRules { get; set; }
    public bool HideQuestionRules { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormType, SurveyFormTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormTypeId));
    }
}
