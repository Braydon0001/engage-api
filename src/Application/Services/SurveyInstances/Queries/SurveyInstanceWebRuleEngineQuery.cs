namespace Engage.Application.Services.SurveyInstances.Queries;

public class SurveyInstanceWebRuleEngineQuery : IRequest<SurveyQuestion>
{
    //the currently displayed questionId
    public int QuestionId { get; set; }
    //the current question's answer Id
    public int SurveyAnswerId { get; set; }
}
public class SurveyInstanceWebRuleEngineHandler : VmQueryHandler, IRequestHandler<SurveyInstanceWebRuleEngineQuery, SurveyQuestion>
{
    private readonly ContactReportSettings _contactReportSettings;
    public SurveyInstanceWebRuleEngineHandler(IAppDbContext context, IMapper mapper, IOptions<ContactReportSettings> contactReportSettings) : base(context, mapper)
    {
        _contactReportSettings = contactReportSettings.Value;
    }

    public async Task<SurveyQuestion> Handle(SurveyInstanceWebRuleEngineQuery request, CancellationToken cancellationToken)
    {
        //get all questions
        var questionsList = await _context.SurveyQuestions
            .AsNoTracking()
            .Where(e => e.SurveyId == _contactReportSettings.ContactReportSurveyId)
            .OrderBy(e => e.DisplayOrder)
            .ToListAsync(cancellationToken);

        //get the next question in the survey
        var currentQuestionIndex = questionsList.FindIndex(e => e.SurveyQuestionId == request.QuestionId);

        int nextQuestion = (currentQuestionIndex + 1) == questionsList.Count
                ? -1
                : questionsList[currentQuestionIndex + 1].SurveyQuestionId;

        if (nextQuestion == -1)
        {
            //return that current question is final question
            return new SurveyQuestion
            {
                SurveyQuestionId = nextQuestion,
            };
        }
        var question = await _context.SurveyQuestions
            .AsNoTracking()
            .Include(e => e.Rules)
            .FirstOrDefaultAsync(e => e.SurveyQuestionId == nextQuestion);

        if (question == null)
        {
            //shouldn't be possible
            return null;
        }
        else if (!question.Rules.Any())
        {
            //return this question as it has no rules
            return question;
        }
        //check rules
        var answer = await _context.SurveyAnswers
            .AsNoTracking()
            .Include(e => e.SurveyAnswerOptions)
            .FirstOrDefaultAsync(e => e.SurveyAnswerId == request.SurveyAnswerId);

        if (question.SurveyQuestionId == _contactReportSettings.QuestionTwoId)
        {
            // next question is question 2
            if (!answer.SurveyAnswerOptions.Any())
            {
                //return question 3
                return questionsList[currentQuestionIndex + 2];
            }
            else
            {
                foreach (var option in answer.SurveyAnswerOptions)
                {
                    if (option.SurveyQuestionOptionId == _contactReportSettings.QuestionTwoAnswerOptionId)
                    {
                        //question 1 answer contains Other
                        return question;
                    }
                }
                //return question 3
                return questionsList[currentQuestionIndex + 2];
            }
        }
        else if (question.SurveyQuestionId == _contactReportSettings.QuestionFiveId)
        {
            // next question is question 5
            if (!answer.SurveyAnswerOptions.Any())
            {
                //return question 6
                return questionsList[currentQuestionIndex + 2];
            }
            else
            {
                foreach (var option in answer.SurveyAnswerOptions)
                {
                    if (option.SurveyQuestionOptionId == _contactReportSettings.QuestionFiveAnswerOptionId)
                    {
                        //question 4 contains other
                        return question;
                    }
                }
                //return question 6
                return questionsList[currentQuestionIndex + 2];
            }
        }
        else if (question.SurveyQuestionId == _contactReportSettings.QuestionTenId
            || question.SurveyQuestionId == _contactReportSettings.QuestionElevenId
            || question.SurveyQuestionId == _contactReportSettings.QuestionTwelveId)
        {
            if (question.Rules.Any())
            {
                var questionNine = await _context.SurveyAnswers
                    .AsNoTracking()
                    .Include(e => e.SurveyAnswerOptions)
                    .FirstOrDefaultAsync(
                        e => e.SurveyQuestionId == question.Rules.First().TargetQuestionId
                        && e.SurveyInstanceId == answer.SurveyInstanceId, cancellationToken);

                if (!questionNine.SurveyAnswerOptions.Any())
                {
                    //no options selected for feedback, survey complete
                    return new SurveyQuestion
                    {
                        SurveyQuestionId = -1
                    };
                }
                if (question.SurveyQuestionId == _contactReportSettings.QuestionTenId)
                {
                    //question 10
                    foreach (var option in questionNine.SurveyAnswerOptions)
                    {
                        if (option.SurveyQuestionOptionId == _contactReportSettings.QuestionTenAnswerOptionId)
                        {
                            //answer 9 contains Rep
                            return question;
                        }
                    }
                    // move on to next question
                    question = await _context.SurveyQuestions
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.SurveyQuestionId == _contactReportSettings.QuestionElevenId, cancellationToken);
                }
                if (question.SurveyQuestionId == _contactReportSettings.QuestionElevenId)
                {
                    //question 11
                    foreach (var option in questionNine.SurveyAnswerOptions)
                    {
                        if (option.SurveyQuestionOptionId == _contactReportSettings.QuestionElevenAnswerOptionId)
                        {
                            //answer 9 contains telesales
                            return question;
                        }
                    }
                    question = await _context.SurveyQuestions
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.SurveyQuestionId == _contactReportSettings.QuestionTwelveId, cancellationToken);
                }
                if (question.SurveyQuestionId == _contactReportSettings.QuestionTwelveId)
                {
                    //question 12
                    foreach (var option in questionNine.SurveyAnswerOptions)
                    {
                        if (option.SurveyQuestionOptionId == _contactReportSettings.QuestionTwelveAnswerOptionId)
                        {
                            //answer 9 contains telesales
                            return question;
                        }
                    }
                }
                return new SurveyQuestion
                {
                    SurveyQuestionId = -1,
                };
            }
            else
            {
                return new SurveyQuestion
                {
                    SurveyQuestionId = -1,
                };
            }
        }

        throw new NotFoundException("no question found", question);
    }
}
