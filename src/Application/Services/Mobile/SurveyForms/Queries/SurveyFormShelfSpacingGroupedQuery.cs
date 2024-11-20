namespace Engage.Application.Services.Mobile.SurveyForms.Queries;

public class SurveyFormShelfSpacingGroupedQuery : IRequest<SurveyFormMobileDto>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
}

public record SurveyFormShelfSpacingGroupedHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormShelfSpacingGroupedQuery, SurveyFormMobileDto>
{
    public async Task<SurveyFormMobileDto> Handle(SurveyFormShelfSpacingGroupedQuery query, CancellationToken cancellationToken)
    {
        var surveyAdvancedTargetingVm = await Mediator.Send(new SurveyFormShelfSpacingTargetedQuery(query.EmployeeId, query.StoreId, DateTime.Now), cancellationToken);
        var surveyList = surveyAdvancedTargetingVm.SurveyForms.Data;

        //get options of all the suppliers
        var suppliers = surveyList.Where(e => e.SupplierId != null).Select(e => new OptionDto() { Name = e.SupplierId.Name, Id = e.SupplierId.Id }).DistinctBy(e => e.Id).ToList();

        //get all the "required" flag surveys
        var requiredSurveyIds = surveyList.Where(e => e.IsRequired).Select(e => e.Id).ToList();

        //get all the surveys that have a required rule and have to be validated
        var requiredRuleSurveyIds = surveyList.Where(e => e.Rules != null && e.Rules.Any(r => r.Type == "Required Rule")).Select(e => e.Id).ToList();

        return new SurveyFormMobileDto() { SurveyFormAdvancedTargeting = surveyAdvancedTargetingVm, Suppliers = suppliers, RequiredSurveyIds = requiredSurveyIds, RequiredRuleSurveyIds = requiredRuleSurveyIds, StoreId = query.StoreId, EmployeeId = query.EmployeeId };
    }
}