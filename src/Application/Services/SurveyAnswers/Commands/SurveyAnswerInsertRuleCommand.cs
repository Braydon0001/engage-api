namespace Engage.Application.Services.SurveyAnswers.Commands;
public class SurveyAnswerInsertRuleCommand : IRequest<SurveyAnswer>
{
    public int SurveyInstanceId { get; set; }
    public int SurveyQuestionId { get; set; }
    public string Answer { get; set; }
    public int? QuestionFalseReason { get; set; }
    public bool? BooleanAnswer { get; set; }
    public List<int> SurveyOptions { get; set; }
}
public class SurveyAnswerInsertRuleHandler : InsertHandler, IRequestHandler<SurveyAnswerInsertRuleCommand, SurveyAnswer>
{
    public SurveyAnswerInsertRuleHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SurveyAnswer> Handle(SurveyAnswerInsertRuleCommand request, CancellationToken cancellationToken)
    {

        //answer is boolean question
        SurveyAnswer answer;
        if (request.BooleanAnswer != null)
        {
            if (request.BooleanAnswer == true)
            {
                answer = new SurveyAnswer
                {
                    SurveyInstanceId = request.SurveyInstanceId,
                    SurveyQuestionId = request.SurveyQuestionId,
                    Answer = request.BooleanAnswer.ToString(),
                };

            }
            else
            {
                answer = new SurveyAnswer
                {
                    SurveyInstanceId = request.SurveyInstanceId,
                    SurveyQuestionId = request.SurveyQuestionId,
                    QuestionFalseReasonId = request.QuestionFalseReason,
                    Answer = request.BooleanAnswer.ToString(),
                };
            }
        }
        else
        {
            answer = new SurveyAnswer
            {
                SurveyInstanceId = request.SurveyInstanceId,
                SurveyQuestionId = request.SurveyQuestionId,
                Answer = request.Answer,
            };
        }

        _context.SurveyAnswers.Add(answer);

        await _context.SaveChangesAsync(cancellationToken);

        if (request.SurveyOptions != null &&
            request.SurveyOptions.Count > 0)
        {
            var answerId = answer.SurveyAnswerId;
            foreach (var item in request.SurveyOptions)
            {
                var entity = new SurveyAnswerOption
                {
                    SurveyAnswerId = answerId,
                    SurveyQuestionOptionId = item,
                };
                _context.SurveyAnswerOptions.Add(entity);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return answer;
    }
}
public class SurveyAnswerInsertRuleValidator : AbstractValidator<SurveyAnswerInsertRuleCommand>
{
    public SurveyAnswerInsertRuleValidator()
    {
        RuleFor(e => e.SurveyInstanceId).NotNull().GreaterThan(0);
        RuleFor(e => e.SurveyQuestionId).NotNull().GreaterThan(0);
        RuleFor(e => e.Answer);
        RuleFor(e => e.QuestionFalseReason);
        RuleFor(e => e.BooleanAnswer);
    }
}