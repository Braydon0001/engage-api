using System.Text.RegularExpressions;

namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupReorderCommand : IRequest<OperationStatus>
{
    public int SurveyId { get; set; }
    public List<OrderedSurveyQuestion> OrderedSurveyGroups { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroupReorderCommand, SurveyFormQuestionGroup>();
    }
}

public record SurveyFormQuestionGroupReorderHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionGroupReorderCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormQuestionGroupReorderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var surveyGroups = await Context.SurveyFormQuestionGroups.Include(e => e.SurveyFormQuestions).Where(e => e.SurveyFormId == command.SurveyId).ToListAsync(cancellationToken);
            var newOrderQuestionGroups = new List<SurveyFormQuestionGroup>();
            foreach (var group in command.OrderedSurveyGroups)
            {
                var surveyGroup = surveyGroups.Where(e => e.SurveyFormQuestionGroupId == group.SurveyFormQuestionGroupId).FirstOrDefault();
                if (surveyGroup == null)
                {
                    throw new Exception("Survey Group Not Found");
                }
                surveyGroup.DisplayOrder = group.NewOrder;
                newOrderQuestionGroups.Add(surveyGroup);
            }

            //after getting the new order, see if any question or group violates its rule references.We know that the order in the groups will not have changed
            var newOrderGroups = newOrderQuestionGroups.OrderBy(e => e.DisplayOrder).ToList();
            foreach (var group in newOrderGroups)
            {
                //first, look at the group rules
                var groupDisplayOrder = group.DisplayOrder;
                var groupRules = group.Rules;
                if (groupRules != null && groupRules.Count > 0)
                {
                    foreach (var rule in groupRules)
                    {
                        var referencedQuestionIds = GetRuleReferencedQuestions(rule);
                        var referencedQuestionGroups = newOrderGroups.Where(e => e.SurveyFormQuestions.Where(q => referencedQuestionIds.Contains(q.SurveyFormQuestionId)).Any()).ToList();
                        var violatedGroups = referencedQuestionGroups.Where(e => e.DisplayOrder > group.DisplayOrder).Select(e => e.SurveyFormQuestionGroupId).ToList();

                        if (violatedGroups.Any())
                        {
                            //var referencedGroups = newOrderGroups.Where(e => violatedGroups.Contains(e.SurveyFormQuestionGroupId)).Select(e => "\nGroup " + e.DisplayOrder + " - " + "\"" + e.Name + "\"").ToList();
                            //var referencedGroupsError = "";
                            //foreach (var referencedGroup in referencedGroups)
                            //{
                            //    referencedGroupsError = referencedGroupsError + referencedGroup;
                            //}
                            var violatedRule = "Rule Reference Error: The " + rule.Type + " rule for Group " + group.DisplayOrder + 1 + " - " + "\"" + group.Name + "\"" + " references questions in a group that would come after it in the new order.";

                            throw new Exception(violatedRule);
                        }
                    }
                }

                //then look at the question rules in that group
                foreach (var question in group.SurveyFormQuestions)
                {
                    var questionDisplayOrder = question.DisplayOrder;
                    var questionRules = question.Rules;
                    if (questionRules != null && questionRules.Count > 0)
                    {
                        foreach (var rule in questionRules)
                        {
                            var referencedQuestionIds = GetRuleReferencedQuestions(rule);
                            var referencedQuestionGroups = newOrderGroups.Where(e => e.SurveyFormQuestions.Where(q => referencedQuestionIds.Contains(q.SurveyFormQuestionId)).Any()).ToList();
                            var violatedGroups = referencedQuestionGroups.Where(e => e.DisplayOrder > question.SurveyFormQuestionGroup.DisplayOrder).Select(e => e.SurveyFormQuestionGroupId).ToList();

                            if (violatedGroups.Any())
                            {
                                //var referencedGroups = newOrderGroups.Where(e => violatedGroups.Contains(e.SurveyFormQuestionGroupId)).Select(e => "\nGroup " + e.DisplayOrder + " - " + "\"" + e.Name + "\"").ToList();
                                //var referencedGroupsError = "";
                                //foreach (var referencedGroup in referencedGroups)
                                //{
                                //    referencedGroupsError = referencedGroupsError + referencedGroup;
                                //}
                                var violatedRule = "Rule Reference Error: The " + rule.Type + " rule for a question (\"" + question.QuestionText + "\") in Group " + question.SurveyFormQuestionGroup.DisplayOrder + " - " + "\"" + question.SurveyFormQuestionGroup.Name + "\"" + " references questions in a group that would come after it in the new order.";

                                throw new Exception(violatedRule);
                            }
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

public class UpdateSurveyFormQuestionGroupReorderValidator : AbstractValidator<SurveyFormQuestionGroupReorderCommand>
{
    public UpdateSurveyFormQuestionGroupReorderValidator()
    {
        RuleFor(x => x.SurveyId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.OrderedSurveyGroups).NotEmpty();
    }
}

public class OrderedSurveyQuestion
{
    public int SurveyFormQuestionGroupId { get; set; }
    public int NewOrder { get; set; }
}