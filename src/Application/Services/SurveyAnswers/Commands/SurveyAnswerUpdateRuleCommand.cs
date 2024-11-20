namespace Engage.Application.Services.SurveyAnswers.Commands;

public class SurveyAnswerUpdateRuleCommand : IRequest<SurveyAnswer>
{
    public int Id { get; set; }
    public int SurveyInstanceId { get; set; }
    public int SurveyQuestionId { get; set; }
    public string Answer { get; set; }
    public int? QuestionFalseReason { get; set; }
    public bool? BooleanAnswer { get; set; }
    public List<int> SurveyOptions { get; set; }
}
public class SurveyAnswerUpdateRuleHandler : UpdateHandler, IRequestHandler<SurveyAnswerUpdateRuleCommand, SurveyAnswer>
{
    private readonly ContactReportSettings _contactReportSettings;
    public SurveyAnswerUpdateRuleHandler(IAppDbContext context, IMapper mapper, IOptions<ContactReportSettings> contactReportSettings) : base(context, mapper)
    {
        _contactReportSettings = contactReportSettings.Value;
    }

    public async Task<SurveyAnswer> Handle(SurveyAnswerUpdateRuleCommand request, CancellationToken cancellationToken)
    {
        var answer = await _context.SurveyAnswers
            .Include(e => e.SurveyAnswerOptions)
            .SingleOrDefaultAsync(e => e.SurveyAnswerId == request.Id, cancellationToken);
        if (answer == null)
        {
            return null;
        }

        //Boolean answer
        if (request.BooleanAnswer != null)
        {
            if (request.BooleanAnswer == false)
            {
                answer.QuestionFalseReasonId = request.QuestionFalseReason;
            }
            else
            {
                answer.QuestionFalseReasonId = null;
            }
            answer.Answer = request.BooleanAnswer.ToString();
        }
        else
        {
            answer.Answer = request.Answer;
        }

        if (request.SurveyOptions != null)
        {
            if (answer.SurveyAnswerOptions.Any())
            {
                _context.SurveyAnswerOptions.RemoveRange(answer.SurveyAnswerOptions);
            }

            foreach (var item in request.SurveyOptions)
            {
                var entity = new SurveyAnswerOption
                {
                    SurveyAnswerId = answer.SurveyAnswerId,
                    SurveyQuestionOptionId = item,
                };
                _context.SurveyAnswerOptions.Add(entity);
            }
        }

        //check if answers need to be deleted
        if (answer.SurveyQuestionId == _contactReportSettings.QuestionOneId)
        {
            //Check if there is an answer for next question and delete if there is
            var nextAnswer = await _context.SurveyAnswers
                .FirstOrDefaultAsync(e => e.SurveyQuestionId == _contactReportSettings.QuestionTwoId
                && e.SurveyInstanceId == answer.SurveyInstanceId,
                cancellationToken);

            if (nextAnswer != null)
            {
                //check if answerOptions contains Other option via its ID
                if (request.SurveyOptions != null &&
                    request.SurveyOptions.Contains(_contactReportSettings.QuestionTwoAnswerOptionId))
                {
                    //don't delete answer
                }
                else
                {
                    //delete answer for question 2
                    _context.SurveyAnswers.Remove(nextAnswer);
                }
            }
            else
            {
                if (request.SurveyOptions != null &&
                    request.SurveyOptions.Contains(_contactReportSettings.QuestionTwoAnswerOptionId))
                {
                    //answer contains other but has no answer for next question
                    var surveyInstance = await _context.SurveyInstances
                                                   .FirstOrDefaultAsync(e =>
                                                       e.SurveyInstanceId == answer.SurveyInstanceId);
                    if (surveyInstance.IsCompleted)
                    {
                        surveyInstance.IsCompleted = false;
                    }
                }
            }
        }
        else if (answer.SurveyQuestionId == _contactReportSettings.QuestionNineId)
        {
            var questionTen = await _context.SurveyAnswers
                .FirstOrDefaultAsync(e => e.SurveyQuestionId == _contactReportSettings.QuestionTenId
                && e.SurveyInstanceId == answer.SurveyInstanceId,
                cancellationToken);

            if (questionTen != null)
            {
                //check if answerOptions contains Rep option via its ID
                if (request.SurveyOptions != null &&
                    request.SurveyOptions.Contains(_contactReportSettings.QuestionTenAnswerOptionId))
                {
                    //don't delete answer
                }
                else
                {
                    //delete answer for question 10
                    _context.SurveyAnswers.Remove(questionTen);
                }
            }
            else
            {
                if (request.SurveyOptions != null && request.SurveyOptions.Contains(_contactReportSettings.QuestionTenAnswerOptionId))
                {
                    //answer contains other but has no answer for next question
                    var surveyInstance = await _context.SurveyInstances
                                                   .FirstOrDefaultAsync(e =>
                                                       e.SurveyInstanceId == answer.SurveyInstanceId);
                    if (surveyInstance.IsCompleted)
                    {
                        surveyInstance.IsCompleted = false;
                    }
                }
            }
            var questionEleven = await _context.SurveyAnswers
                .FirstOrDefaultAsync(e => e.SurveyQuestionId == _contactReportSettings.QuestionElevenId
                && e.SurveyInstanceId == answer.SurveyInstanceId,
                cancellationToken);

            if (questionEleven != null)
            {
                //check if answerOptions contains Telesales option via its ID
                if (request.SurveyOptions != null &&
                    request.SurveyOptions.Contains(_contactReportSettings.QuestionElevenAnswerOptionId))
                {
                    //don't delete answer
                }
                else
                {
                    //delete answer for question 11
                    _context.SurveyAnswers.Remove(questionEleven);
                }
            }
            else
            {
                if (request.SurveyOptions != null &&
                    request.SurveyOptions.Contains(_contactReportSettings.QuestionElevenAnswerOptionId))
                {
                    //answer contains other but has no answer for next question
                    var surveyInstance = await _context.SurveyInstances
                                                   .FirstOrDefaultAsync(e =>
                                                       e.SurveyInstanceId == answer.SurveyInstanceId);
                    if (surveyInstance.IsCompleted)
                    {
                        surveyInstance.IsCompleted = false;
                    }
                }
            }
            var questionTwelve = await _context.SurveyAnswers
                .FirstOrDefaultAsync(e => e.SurveyQuestionId == _contactReportSettings.QuestionTwelveId
                && e.SurveyInstanceId == answer.SurveyInstanceId,
                cancellationToken);

            if (questionTwelve != null)
            {
                //check if answerOptions contains Manger option via its ID
                if (request.SurveyOptions != null &&
                    request.SurveyOptions.Contains(_contactReportSettings.QuestionTwelveAnswerOptionId))
                {
                    //don't delete answer
                }
                else
                {
                    //delete answer for question 12
                    _context.SurveyAnswers.Remove(questionTwelve);
                }
            }
            else
            {
                if (request.SurveyOptions != null &&
                    request.SurveyOptions.Contains(_contactReportSettings.QuestionTwelveAnswerOptionId))
                {
                    //answer contains other but has no answer for next question
                    var surveyInstance = await _context.SurveyInstances
                                               .FirstOrDefaultAsync(e =>
                                                   e.SurveyInstanceId == answer.SurveyInstanceId);
                    if (surveyInstance.IsCompleted)
                    {
                        surveyInstance.IsCompleted = false;
                    }
                }
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return answer;
    }
}
public class SurveyAnswerUpdateRuleValidator : AbstractValidator<SurveyAnswerUpdateRuleCommand>
{
    public SurveyAnswerUpdateRuleValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyInstanceId).NotNull().GreaterThan(0);
        RuleFor(e => e.SurveyQuestionId).NotNull().GreaterThan(0);
        RuleFor(e => e.Answer);
        RuleFor(e => e.QuestionFalseReason);
        RuleFor(e => e.BooleanAnswer);
    }
}