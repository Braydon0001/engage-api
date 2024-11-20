using Engage.Application.Services.SurveyInstances.Models;

namespace Engage.Application.Services.SurveyInstances.Queries;

public class SurveyInstanceNextPreviousQuestionQuery : IRequest<SurveyInstanceWebNextPreviousQuestionDto>
{
    public int SurveyInstanceId { get; set; }
    //currently displayed question
    public int QuestionId { get; set; }
}
public class SurveyInstanceNextPreviousQuestionHandler : BaseQueryHandler, IRequestHandler<SurveyInstanceNextPreviousQuestionQuery, SurveyInstanceWebNextPreviousQuestionDto>
{
    public SurveyInstanceNextPreviousQuestionHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SurveyInstanceWebNextPreviousQuestionDto> Handle(SurveyInstanceNextPreviousQuestionQuery request, CancellationToken cancellationToken)
    {
        if (request.SurveyInstanceId == 0)
        {
            throw new Exception("surveyInstance not found");
        }
        var surveyInstance = await _context.SurveyInstances
            .Where(s => s.SurveyInstanceId == request.SurveyInstanceId)
            .FirstOrDefaultAsync(cancellationToken);

        var survey = await _context.Surveys
            .AsNoTracking()
            .Where(e => e.SurveyId == surveyInstance.SurveyId)
            .Include(q => q.SurveyQuestions)
            .FirstOrDefaultAsync(cancellationToken);

        var employeeStore = await _context.EmployeeStoreCalendars
                                .AsNoTracking()
                                .FirstOrDefaultAsync(e =>
                                e.SurveyInstanceId == surveyInstance.SurveyInstanceId, cancellationToken);

        int nextQuestion;
        int prevousQuestion = -1;
        int questionNumber;

        var questionsList = survey.SurveyQuestions.ToList();

        if (request.QuestionId == 0)
        {
            nextQuestion = questionsList.First().SurveyQuestionId;
            prevousQuestion = questionsList.First().SurveyQuestionId;
            questionNumber = 1;
        }
        else
        {
            var currentQuestionIndex = questionsList.FindIndex(e => e.SurveyQuestionId == request.QuestionId);
            nextQuestion = (currentQuestionIndex + 1) == questionsList.Count
                ? -1
                : questionsList[currentQuestionIndex + 1].SurveyQuestionId;
            questionNumber = currentQuestionIndex + 1;

            if (currentQuestionIndex == 0)
            {
                prevousQuestion = questionsList.First().SurveyQuestionId;
            }
            else
            {
                for (int i = currentQuestionIndex - 1; i >= 0; i--)
                {
                    var answer = await _context.SurveyAnswers
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.SurveyQuestionId == questionsList[i].SurveyQuestionId
                        && e.SurveyInstanceId == request.SurveyInstanceId);
                    if (answer != null)
                    {
                        prevousQuestion = answer.SurveyQuestionId;
                        break;
                    }
                }
                if (prevousQuestion == -1)
                {
                    //shouldn't be possible
                    prevousQuestion = questionsList.First().SurveyQuestionId;
                }
            }
        }

        return new SurveyInstanceWebNextPreviousQuestionDto
        {
            NextQuestionId = nextQuestion,
            PreviousQuestionId = prevousQuestion,
            QuestionNumber = questionNumber,
            TotalQuestions = questionsList.Count,
            SurveyTitle = survey.Title,
            IsCompleted = surveyInstance.IsCompleted,
            Date = employeeStore.CalendarDate.ToShortDateString(),
        };
    }
}
