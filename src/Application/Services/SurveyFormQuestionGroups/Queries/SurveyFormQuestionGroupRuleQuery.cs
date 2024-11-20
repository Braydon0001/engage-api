namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public record SurveyFormQuestionGroupRuleQuery(int GroupId, string RuleType) : IRequest<JsonRule>;

public record SurveyFormQuestionGroupRuleHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionGroupRuleQuery, JsonRule>
{
    public async Task<JsonRule> Handle(SurveyFormQuestionGroupRuleQuery query, CancellationToken cancellationToken)
    {
        var groupRules = await Context.SurveyFormQuestionGroups.Where(e => e.SurveyFormQuestionGroupId == query.GroupId).Select(e => e.Rules).FirstOrDefaultAsync(cancellationToken);

        if (groupRules == null || !groupRules.Any())
        {
            return null;
        }

        var rule = groupRules.Where(e => e.Type == query.RuleType).FirstOrDefault();

        if (rule == null)
        {
            return null;
        }

        return rule;
    }
}