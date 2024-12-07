﻿namespace Engage.Application.Services.Mobile.SurveyForms.Queries;

public class SurveyFormStoreConceptMobileQuery : IRequest<SurveyFormMobileHistoryDto>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
}

public record SurveyFormStoreConceptMobileHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormStoreConceptMobileQuery, SurveyFormMobileHistoryDto>
{
    public async Task<SurveyFormMobileHistoryDto> Handle(SurveyFormStoreConceptMobileQuery query, CancellationToken cancellationToken)
    {
        var exisitingFormIds = await Context.SurveyFormSubmissions
                                            .Where(e => e.StoreId == query.StoreId
                                            && e.SurveyForm.SurveyFormTypeId == (int)SurveyFormTypeId.Concept)
                                            .Select(e => e.SurveyFormId).ToListAsync(cancellationToken);


        var result = await Mediator.Send(new SurveyFormStoreConceptTargetedMobileQuery { EmployeeId = query.EmployeeId, StoreId = query.StoreId, ExistingFormIds = exisitingFormIds }, cancellationToken);

        var surveyAdvancedTargetingVm = result.SurveyFormAdvancedTargetingVm;

        var surveyList = surveyAdvancedTargetingVm.SurveyForms.Data;

        //get all the "required" flag surveys
        var requiredSurveyIds = surveyList.Where(e => e.IsRequired).Select(e => e.Id).ToList();

        //get all the surveys that have a required rule and have to be validated
        var requiredRuleSurveyIds = surveyList.Where(e => e.Rules != null && e.Rules.Any(r => r.Type == "Required Rule")).Select(e => e.Id).ToList();

        surveyAdvancedTargetingVm.SurveyForms.Data = surveyList;
        surveyAdvancedTargetingVm.SurveyForms.Count = surveyList.Count;

        return new SurveyFormMobileHistoryDto()
        {
            SurveyFormAdvancedTargeting = surveyAdvancedTargetingVm,
            RequiredSurveyIds = requiredSurveyIds,
            RequiredRuleSurveyIds = requiredRuleSurveyIds,
            StoreId = query.StoreId,
            EmployeeId = query.EmployeeId,
            History = result.SurveyFormWithHistory
        };
    }
}
