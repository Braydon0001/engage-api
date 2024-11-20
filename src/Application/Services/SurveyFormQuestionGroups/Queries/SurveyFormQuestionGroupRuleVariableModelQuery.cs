using Engage.Application.Services.SurveyFormQuestions.Queries;

namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public record SurveyFormQuestionGroupRuleVariableModelQuery(int GroupId) : IRequest<VariablesWithOptions>;

public record SurveyFormQuestionGroupRuleVariableModelHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormQuestionGroupRuleVariableModelQuery, VariablesWithOptions>
{
    public async Task<VariablesWithOptions> Handle(SurveyFormQuestionGroupRuleVariableModelQuery query, CancellationToken cancellationToken)
    {
        var firstQuestionInGroup = await Context.SurveyFormQuestionGroups
                                            .Where(e => e.SurveyFormQuestionGroupId == query.GroupId)
                                            .Select(e => e.SurveyFormQuestions.OrderBy(q => q.DisplayOrder)
                                                .Select(e => e.SurveyFormQuestionId)
                                                .FirstOrDefault())
                                            .FirstOrDefaultAsync(cancellationToken);

        return await Mediator.Send(new SurveyFormQuestionRuleVariableModelQuery(firstQuestionInGroup, false, false), cancellationToken); ;
    }
}