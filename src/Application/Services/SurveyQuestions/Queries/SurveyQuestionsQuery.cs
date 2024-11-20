using Engage.Application.Services.SurveyQuestions.Models;

namespace Engage.Application.Services.SurveyQuestions.Queries;

public class SurveyQuestionsQuery : GetQuery, IRequest<ListResult<SurveyQuestionListItemDto>>
{
    public int? SurveyId { get; set; }
}

public class SurveyQuestionsQueryHandler : BaseQueryHandler, IRequestHandler<SurveyQuestionsQuery, ListResult<SurveyQuestionListItemDto>>
{
    public SurveyQuestionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<SurveyQuestionListItemDto>> Handle(SurveyQuestionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.SurveyQuestions.Where(e => e.SurveyId == request.SurveyId)
                                                     .OrderBy(e => e.DisplayOrder)
                                                     .ProjectTo<SurveyQuestionListItemDto>(_mapper.ConfigurationProvider)
                                                     .ToListAsync(cancellationToken);

        return new ListResult<SurveyQuestionListItemDto>(entities);
    }
}
