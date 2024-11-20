using Engage.Application.Services.SurveyAnswers.Models;
using Engage.Application.Services.SurveyQuestions.Models;

namespace Engage.Application.Services.SurveyAnswers.Queries;

public class SurveyAnswerCurrentQuestionQuery : IRequest<SurveyAnswerCurrentQuestionDto>
{
    public int SurveyInstanceId { get; set; }
    public int QuestionId { get; set; }
}
public class SurveyAnswerCurrentQuestionQueryHandler : BaseQueryHandler, IRequestHandler<SurveyAnswerCurrentQuestionQuery, SurveyAnswerCurrentQuestionDto>
{
    public SurveyAnswerCurrentQuestionQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SurveyAnswerCurrentQuestionDto> Handle(SurveyAnswerCurrentQuestionQuery request, CancellationToken cancellationToken)
    {
        int questionId = request.QuestionId;
        if (request.QuestionId == 0)
        {
            //pass first question
            var surveyInstance = await _context.SurveyInstances
                                    .AsNoTracking()
                                    .Where(e => e.SurveyInstanceId == request.SurveyInstanceId)
                                    .FirstOrDefaultAsync(cancellationToken);
            var survey = await _context.Surveys.AsNoTracking()
                .Include(e => e.SurveyQuestions)
                .Where(e => e.SurveyId == surveyInstance.SurveyId)
                .FirstOrDefaultAsync(cancellationToken);

            var questionList = survey.SurveyQuestions.ToList();

            var firstQuestion = questionList.First();

            questionId = firstQuestion.SurveyQuestionId;
        }
        var answer = await _context.SurveyAnswers
            .AsNoTracking()
            .Where(e => e.SurveyInstanceId == request.SurveyInstanceId
                && e.SurveyQuestionId == questionId)
            .ProjectTo<SurveyAnswerCurrentQuestionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (answer == null)
        {
            //question not answered before
            var question = await _context.SurveyQuestions
                .AsNoTracking()
                .Where(e => e.SurveyQuestionId == questionId)
                .ProjectTo<SurveyQuestionListItemDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            List<OptionDto> options = new List<OptionDto>();
            foreach (var item in question.SurveyQuestionOptions)
            {
                options.Add(new OptionDto
                {
                    Id = item.Id,
                    Name = item.Option,
                    Disabled = false,
                });
            }

            answer = new SurveyAnswerCurrentQuestionDto
            {
                QuestionId = question.Id,
                QuestionText = question.Question,
                QuestionType = question.QuestionType,
                IsRequired = question.IsRequired,
                SurveyQuestionOptions = options,
                QuestionFalseReasons = question.QuestionFalseReasons.ToList(),
            };
        }

        return answer;
    }
}