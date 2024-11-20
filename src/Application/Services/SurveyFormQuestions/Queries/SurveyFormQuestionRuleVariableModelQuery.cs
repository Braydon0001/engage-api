namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public record SurveyFormQuestionRuleVariableModelQuery(int QuestionId, bool ReferenceSelf, bool ExcludeSelfReason) : IRequest<VariablesWithOptions>;

public record SurveyFormQuestionRuleVariableModelHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionRuleVariableModelQuery, VariablesWithOptions>
{
    public async Task<VariablesWithOptions> Handle(SurveyFormQuestionRuleVariableModelQuery query, CancellationToken cancellationToken)
    {
        var surveyQuestion = await Context.SurveyFormQuestions.Include(e => e.SurveyFormQuestionGroup).Where(e => e.SurveyFormQuestionId == query.QuestionId).FirstOrDefaultAsync(cancellationToken);

        var questionsFromPrecedingAndOwnGroup = await Context.SurveyFormQuestions
                                                            .Include(e => e.SurveyFormQuestionGroup)
                                                            .Include(e => e.SurveyFormQuestionType)
                                                            .Include(e => e.SurveyFormQuestionReasons)
                                                               .ThenInclude(e => e.SurveyFormReason)
                                                            .Include(e => e.SurveyFormQuestionOptions)
                                                               .ThenInclude(e => e.SurveyFormOption)
                                                            .Where(e => e.SurveyFormQuestionGroup.DisplayOrder <= surveyQuestion.SurveyFormQuestionGroup.DisplayOrder
                                                                && e.SurveyFormQuestionGroup.SurveyFormId == surveyQuestion.SurveyFormQuestionGroup.SurveyFormId
                                                                && !e.Disabled && !e.Deleted)
                                                            .OrderBy(e => e.SurveyFormQuestionGroup.DisplayOrder)
                                                                .ThenBy(e => e.DisplayOrder)
                                                            .ToListAsync(cancellationToken);

        var succedingQuestions = questionsFromPrecedingAndOwnGroup
                                    .Where(e => e.SurveyFormQuestionGroupId == surveyQuestion.SurveyFormQuestionGroupId && e.DisplayOrder > (query.ReferenceSelf ? surveyQuestion.DisplayOrder : surveyQuestion.DisplayOrder - 1))
                                    .ToList();

        var precedingQuestions = questionsFromPrecedingAndOwnGroup.Where(e => !succedingQuestions.Contains(e)).ToList();

        var completeVariableModel = new Dictionary<string, Variable>();
        var completeVariableModelAnswerOptions = new Dictionary<string, List<VariableOption>>();

        foreach (var question in precedingQuestions)
        {
            var questionDataType = "";

            switch (question.SurveyFormQuestionType.Name)
            {
                case "Text":
                    questionDataType = "text"; break;
                case "True/False":
                    questionDataType = "boolean"; break;
                case "Checkbox":
                case "Radio":
                case "Photo":
                    questionDataType = "object"; break;
                case "Date":
                    questionDataType = "date"; break;
                case "Number":
                case "Currency":
                    questionDataType = "number"; break;
                default:
                    throw new ArgumentException("Unknown Data Type");
            }
            //we need to create a variable for each question and one for reason
            var questionVariable = new Variable()
            {
                Name = "q" + question.SurveyFormQuestionId.ToString(),
                DisplayValue = "G" + question.SurveyFormQuestionGroup.DisplayOrder.ToString() + "Q" + question.DisplayOrder.ToString() + ": " + question.QuestionText,
                DataType = questionDataType
            };
            completeVariableModel.Add("q" + question.SurveyFormQuestionId.ToString(), questionVariable);

            //if the question is a checkbox or radio, we need to give the options for the rule engine

            if ((question.SurveyFormQuestionType.Name == "Checkbox" || question.SurveyFormQuestionType.Name == "Radio") && question.SurveyFormQuestionOptions != null && question.SurveyFormQuestionOptions.Any())
            {
                var questionAnswerOptions = new List<VariableOption>(question.SurveyFormQuestionOptions.Select(o => new VariableOption() { Id = o.SurveyFormOptionId, Name = o.SurveyFormOption.Name }).ToList());
                completeVariableModelAnswerOptions.Add(questionVariable.Name, questionAnswerOptions);
            }

            //if the question has reasons, we need to give options to the rule engine

            if (question.SurveyFormQuestionReasons != null && question.SurveyFormQuestionReasons.Any())
            {
                if (query.ExcludeSelfReason && question.SurveyFormQuestionId == surveyQuestion.SurveyFormQuestionId)
                {
                    break;
                }
                var questionReasonVariable = new Variable()
                {
                    Name = "q" + question.SurveyFormQuestionId.ToString() + "r",
                    DisplayValue = "Reason for " + "G" + question.SurveyFormQuestionGroup.DisplayOrder.ToString() + "Q" + question.DisplayOrder.ToString() + ": " + question.QuestionText,
                    DataType = "object"
                };
                completeVariableModel.Add("q" + question.SurveyFormQuestionId.ToString() + "r", questionReasonVariable);
                var questionAnswerReasons = new List<VariableOption>(question.SurveyFormQuestionReasons.Select(r => new VariableOption() { Id = r.SurveyFormReasonId, Name = r.SurveyFormReason.Name }).ToList());
                completeVariableModelAnswerOptions.Add(questionReasonVariable.Name, questionAnswerReasons);
            }
        }

        return new VariablesWithOptions() { CompleteVariableModel = completeVariableModel, Options = completeVariableModelAnswerOptions };
    }
}

public class VariablesWithOptions
{
    public Dictionary<string, Variable> CompleteVariableModel { get; set; }
    public Dictionary<string, List<VariableOption>> Options { get; set; }
}

public class Variable
{
    public string Name { get; set; }
    public string DisplayValue { get; set; }
    public string DataType { get; set; }
}

public class VariableOption
{
    public int Id { get; set; }
    public string Name { get; set; }
}