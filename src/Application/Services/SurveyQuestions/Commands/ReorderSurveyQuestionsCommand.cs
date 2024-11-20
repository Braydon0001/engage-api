using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Engage.Application.Services.SurveyQuestions.Commands;

public class OrderedSurveyQuestion
{
    public int SurveyQuestionId { get; set; }
    public int NewOrder { get; set; }
}

public class ReorderSurveyQuestion
{
    public ReorderSurveyQuestion(int surveyQuestionId, int order)
    {
        SurveyQuestionId = surveyQuestionId;
        Order = order;
    }

    public int SurveyQuestionId { get; private set; }
    public int Order { get; private set; }
}

public class ReorderSurveyQuestionsCommand : IRequest<OperationStatus>
{
    public int SurveyId { get; set; }
    public List<OrderedSurveyQuestion> OrderedSurveyQuestions { get; set; }
}

public class ReorderSurveyQuestionsCommandHandler : BaseUpdateCommandHandler, IRequestHandler<ReorderSurveyQuestionsCommand, OperationStatus>
{
    public ReorderSurveyQuestionsCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(ReorderSurveyQuestionsCommand request, CancellationToken cancellationToken)
    {

        //TODO The configured execution strategy 'MySqlRetryingExecutionStrategy'
        //     does not support user-initiated transactions.
        //     Use the execution strategy returned by 'DbContext.Database.CreateExecutionStrategy()'
        //     to execute all the operations in the transaction as a retriable unit."
        //using var transaction = _context.BeginTransaction();

        try
        {  
            var reorderQuestions = new List<ReorderSurveyQuestion>();
            var reorderQuestionIds = new List<int>();
            if (request.OrderedSurveyQuestions.Count > 0)
            {
                var index = 0;
                foreach (var question in request.OrderedSurveyQuestions)
                {
                    reorderQuestions.Add(new ReorderSurveyQuestion(question.SurveyQuestionId, index + 1));
                    index++;
                }
                reorderQuestionIds = reorderQuestions.Select(e => e.SurveyQuestionId).ToList();
            }

            if (reorderQuestionIds.Count <= 0)
            {
                return new OperationStatus
                {
                    Status = false,
                    OperationId = request.SurveyId

                };
            }

            // Update the questions to their new order
            if (reorderQuestionIds.Count > 0)
            {
                var questions = await _context.SurveyQuestions.Where(e => e.SurveyId == request.SurveyId &&
                                                                         reorderQuestionIds.Contains(e.SurveyQuestionId))
                                                              .Include(e => e.Rules)
                                                              .ToListAsync(cancellationToken);

                foreach (var question in questions)
                {
                    question.DisplayOrder = reorderQuestions.Single(e => e.SurveyQuestionId == question.SurveyQuestionId).Order;
                }

                var violatedRules = new List<SurveyQuestionRule>();

                foreach (var questionId in reorderQuestionIds)
                {
                    var question = questions.Where(e => e.SurveyQuestionId == questionId).FirstOrDefault();
                    var followingQuestionIds = questions.Where(e => e.DisplayOrder > question.DisplayOrder).Select(e => e.SurveyQuestionId).ToList();
                    var violatedDependentRules = question.Rules.Where(x => followingQuestionIds.Contains(x.TargetQuestionId));
                    violatedRules.AddRange(violatedDependentRules);
                }

                if(violatedRules.Count > 0)
                {
                    var violatedRulesText = "";
                    foreach (var rule in violatedRules)
                    {
                        violatedRulesText += $"\n - Question \"{rule.Question.Question}\" has a rule that refers to \"{rule.TargetQuestion.Question}\"";
                    }
                    throw new Exception($"Some questions have rules that point to questions that come after them in the new order: {violatedRulesText}");
                }

                _context.SurveyQuestions.UpdateRange(questions);
                await _context.SaveChangesAsync(cancellationToken);
            }

            //TODO await transaction.CommitAsync(cancellationToken);

            return new OperationStatus
            {
                Status = true,
                OperationId = request.SurveyId

            };
        }
        catch (Exception ex)
        {
            return OperationStatus.CreateFromException($"Reorder Survey Questions Error: \n{ex.Message}", ex);
        }
    }
}
