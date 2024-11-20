using System.Data;
using System.Text.RegularExpressions;

namespace Engage.Application.Services.SurveyFormQuestions.Commands;

public class SurveyFormQuestionReorderCommand : IRequest<OperationStatus>
{
    public int SurveyFormQuestionGroupId { get; set; }
    public List<OrderedSurveyQuestion> OrderedSurveyQuestions { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionReorderCommand, SurveyFormQuestion>();
    }
}

public record SurveyFormQuestionReorderHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionReorderCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormQuestionReorderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var groupQuestions = await Context.SurveyFormQuestions.Where(e => e.SurveyFormQuestionGroupId == command.SurveyFormQuestionGroupId).ToListAsync(cancellationToken);
            var newOrderGroupQuestions = new List<SurveyFormQuestion>();
            foreach (var question in command.OrderedSurveyQuestions)
            {
                var surveyQuestion = groupQuestions.Where(e => e.SurveyFormQuestionId == question.SurveyFormQuestionId).FirstOrDefault();
                if (surveyQuestion == null)
                {
                    throw new Exception("Question Not Found");
                }
                surveyQuestion.DisplayOrder = question.NewOrder;
                newOrderGroupQuestions.Add(surveyQuestion);
            }

            //after getting the new order, see if any question violates its rule references
            var newOrderQuestions = newOrderGroupQuestions.OrderBy(e => e.DisplayOrder).ToList();

            foreach (var question in newOrderQuestions)
            {
                var questionDisplayOrder = question.DisplayOrder;
                var questionRules = question.Rules;
                if (questionRules != null && questionRules.Any())
                {
                    foreach (var rule in questionRules)
                    {
                        var referencedQuestionIds = GetRuleReferencedQuestions(rule);
                        var referencedQuestionDisplayOrders = newOrderQuestions.Where(e => referencedQuestionIds.Contains(e.SurveyFormQuestionId)).Select(e => e.DisplayOrder).ToList();
                        var violatedDisplayOrders = referencedQuestionDisplayOrders.Where(e => e > questionDisplayOrder).ToList();

                        if (violatedDisplayOrders.Any())
                        {
                            var referencedQuestions = newOrderQuestions.Where(e => violatedDisplayOrders.Contains(e.DisplayOrder)).Select(e => e.QuestionText).ToList();
                            var referencedQuestionError = "";
                            foreach (var referencedQuestion in referencedQuestions)
                            {
                                referencedQuestionError = referencedQuestionError + "\"" + referencedQuestion + "\"";
                            }
                            var violatedRule = "Rule Reference Error: The " + rule.Type + " rule for question \"" + question.QuestionText + "\" references questions that would come after it in the new order. The referenced questions are:" + referencedQuestionError;

                            throw new Exception(violatedRule);
                        }
                    }
                }
            }

            var opStatus = await Context.SaveChangesAsync(cancellationToken);

            return opStatus;
        }
        catch (Exception ex)
        {
            return OperationStatus.CreateFromException($"Reorder Survey Questions Error: \n{ex.Message}", ex);
        }
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

public class UpdateSurveyFormQuestionReorderValidator : AbstractValidator<SurveyFormQuestionReorderCommand>
{
    public UpdateSurveyFormQuestionReorderValidator()
    {
        RuleFor(x => x.SurveyFormQuestionGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.OrderedSurveyQuestions).NotEmpty();
    }
}

public class OrderedSurveyQuestion
{
    public int SurveyFormQuestionId { get; set; }
    public int NewOrder { get; set; }
}