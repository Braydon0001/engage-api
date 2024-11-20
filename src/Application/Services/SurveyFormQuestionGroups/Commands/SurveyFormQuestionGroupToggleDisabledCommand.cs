using System.Text.RegularExpressions;

namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupToggleDisabledCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }

}

public record SurveyFormQuestionGroupToggleDisabledHandler(IAppDbContext Context, IMediator Mediator) : IRequestHandler<SurveyFormQuestionGroupToggleDisabledCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormQuestionGroupToggleDisabledCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestionGroups
                                    .Include(e => e.SurveyFormQuestions)
                                    .Where(e => e.SurveyFormQuestionGroupId == command.Id)
                                    .FirstOrDefaultAsync(cancellationToken);

        if (entity != null)
        {
            if (!entity.Disabled)
            {
                //get all the questionIds in this group
                var questionIds = entity.SurveyFormQuestions.Select(e => e.SurveyFormQuestionId).ToList();

                //get all succeeding groups
                var succeedingGroups = await Context.SurveyFormQuestionGroups.Include(e => e.SurveyFormQuestions)
                                                                             .Where(e => e.SurveyFormId == entity.SurveyFormId
                                                                                         && e.DisplayOrder > entity.DisplayOrder)
                                                                             .ToListAsync(cancellationToken);

                if (succeedingGroups.Any())
                {
                    foreach (var group in succeedingGroups)
                    {
                        var groupRules = group.Rules;
                        if (groupRules != null)
                        {
                            foreach (var rule in groupRules)
                            {
                                var referencedQuestions = GetRuleReferencedQuestions(rule);
                                if (referencedQuestions != null && referencedQuestions.Intersect(questionIds).Any())
                                {
                                    throw new Exception("This Group cannot be disabled. Another group (\"" + group.Name + "\") references it in its rules");
                                }
                            }
                        }
                        foreach (var question in group.SurveyFormQuestions)
                        {
                            var succeedingQuestionRules = question.Rules;
                            if (succeedingQuestionRules != null)
                            {
                                foreach (var rule in succeedingQuestionRules)
                                {
                                    var referencedQuestions = GetRuleReferencedQuestions(rule);
                                    if (referencedQuestions != null && referencedQuestions.Intersect(questionIds).Any())
                                    {
                                        throw new Exception("This Group cannot be disabled. A question in it is referenced in another questions' (\"" + question.QuestionText + "\") rules");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            await Mediator.Send(new DeleteCommand
            {
                EntityName = "surveyformquestiongroup",
                Id = command.Id,
                Toggle = true
            });
        }
        var opStatus = await Context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }

    public static List<int> GetRuleReferencedQuestions(JsonRule rule)
    {
        var pattern = @",\""name\"":\""q(\d+)(r)?\"",";

        MatchCollection questionNames = rule.Value.Matches(pattern);

        var referencedQuestions = new List<int>();

        foreach (Match questionName in questionNames)
        {
            var questionIdString = questionName.Groups[1].Value;
            if (String.IsNullOrEmpty(questionIdString))
            {
                throw new Exception("Cannot extract Question Id");
            }
            var questionId = Int32.Parse(questionIdString);
            referencedQuestions.Add(questionId);
        }

        return referencedQuestions;
    }
}

public class SurveyFormQuestionGroupToggleDisabledValidator : AbstractValidator<SurveyFormQuestionGroupToggleDisabledCommand>
{
    public SurveyFormQuestionGroupToggleDisabledValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}