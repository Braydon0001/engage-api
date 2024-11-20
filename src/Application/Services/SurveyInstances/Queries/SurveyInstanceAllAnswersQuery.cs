using Engage.Application.Services.SurveyInstances.Models;

namespace Engage.Application.Services.SurveyInstances.Queries;

public class SurveyInstanceAllAnswersQuery : IRequest<ListResult<SurveyInstanceWebAllAnswersDto>>
{
    public int Id { get; set; }
}
public class SurveyInstanceAllAnswersHandler : VmQueryHandler, IRequestHandler<SurveyInstanceAllAnswersQuery, ListResult<SurveyInstanceWebAllAnswersDto>>
{
    public SurveyInstanceAllAnswersHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<SurveyInstanceWebAllAnswersDto>> Handle(SurveyInstanceAllAnswersQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyAnswers
            .AsNoTracking()
            .Where(e => e.SurveyInstanceId == request.Id)
            .OrderBy(e => e.SurveyQuestion.DisplayOrder)
            .ProjectTo<SurveyInstanceWebAllAnswersDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (entity == null)
        {
            return null;
        }

        //create a list of the surveyAnswersOptions
        foreach (var answer in entity)
        {
            //ignore casing for safety
            if (answer.QuestionType.Equals("Checkbox", StringComparison.OrdinalIgnoreCase))
            {
                var answerOptionsId = await _context.SurveyAnswerOptions
                                      .AsNoTracking()
                                      .Include(e => e.SurveyQuestionOption)
                                      .Where(e => e.SurveyAnswerId == answer.AnswerId)
                                      .ToListAsync(cancellationToken);
                if (answerOptionsId == null)
                    continue;

                List<string> answerList = new List<string>();
                foreach (var option in answerOptionsId)
                {
                    answerList.Add(option.SurveyQuestionOption.Option);
                }
                answer.AnswerOptions = answerList;
            }
        }

        return new ListResult<SurveyInstanceWebAllAnswersDto>
        {
            Count = entity.Count,
            Data = entity
        };
    }
}