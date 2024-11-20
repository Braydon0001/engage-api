namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public record SurveyFormQuestionRuleQuery(int QuestionId, string RuleType) : IRequest<JsonRule>;

public record SurveyFormQuestionRuleHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionRuleQuery, JsonRule>
{
    public async Task<JsonRule> Handle(SurveyFormQuestionRuleQuery query, CancellationToken cancellationToken)
    {
        var questionRules = await Context.SurveyFormQuestions.Where(e => e.SurveyFormQuestionId == query.QuestionId).Select(e => e.Rules).FirstOrDefaultAsync(cancellationToken);

        if (questionRules == null || !questionRules.Any())
        {
            return null;
        }

        var rule = questionRules.Where(e => e.Type == query.RuleType).FirstOrDefault();

        if (rule == null)
        {
            return null;
        }

        return rule;
    }
}