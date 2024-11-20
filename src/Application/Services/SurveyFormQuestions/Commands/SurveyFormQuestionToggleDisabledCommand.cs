using System.Text.RegularExpressions;

namespace Engage.Application.Services.SurveyFormQuestions.Commands;

public class SurveyFormQuestionToggleDisabledCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }

}

public record SurveyFormQuestionToggleDisabledHandler(IAppDbContext Context, IMediator Mediator) : IRequestHandler<SurveyFormQuestionToggleDisabledCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormQuestionToggleDisabledCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestions
                                    .Include(e => e.SurveyFormQuestionGroup)
                                    .Where(e => e.SurveyFormQuestionId == command.Id)
                                    .FirstOrDefaultAsync(cancellationToken);

        if (entity != null)
        {
            if (!entity.Disabled)
            {
                //get all succeeding question in own group
                var succeedingQuestions = await Context.SurveyFormQuestions.Where(e => e.SurveyFormQuestionGroupId == entity.SurveyFormQuestionGroupId
                                                                                            && e.DisplayOrder > entity.DisplayOrder)
                                                                                .ToListAsync(cancellationToken);

                //throw an error if the question to be disabled is referenced in another question
                if (succeedingQuestions.Any())
                {
                    foreach (var question in succeedingQuestions)
                    {
                        var succeedingQuestionRules = question.Rules;
                        if (succeedingQuestionRules != null)
                        {
                            foreach (var rule in succeedingQuestionRules)
                            {
                                var referencedQuestions = GetRuleReferencedQuestions(rule);
                                if (referencedQuestions != null && referencedQuestions.Contains(entity.SurveyFormQuestionId))
                                {
                                    throw new Exception("This Question cannot be deleted. It is referenced in another questions' (\"" + question.QuestionText + "\") rules");
                                }
                            }
                        }
                    }
                }

                //get all succeeding groups
                var succeedingGroups = await Context.SurveyFormQuestionGroups.Include(e => e.SurveyFormQuestions)
                                                                             .Where(e => e.SurveyFormId == entity.SurveyFormQuestionGroup.SurveyFormId
                                                                                         && e.DisplayOrder > entity.SurveyFormQuestionGroup.DisplayOrder)
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
                                if (referencedQuestions != null && referencedQuestions.Contains(entity.SurveyFormQuestionId))
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
                                    if (referencedQuestions != null && referencedQuestions.Contains(entity.SurveyFormQuestionId))
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
                EntityName = "surveyformquestion",
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

public class SurveyFormQuestionToggleDisabledValidator : AbstractValidator<SurveyFormQuestionToggleDisabledCommand>
{
    public SurveyFormQuestionToggleDisabledValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}