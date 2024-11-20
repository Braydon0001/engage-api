using Engage.Application.Services.SurveyAnswers.Models;

namespace Engage.Application.Services.SurveyAnswers.Queries
{
    public class SurveyAnswersQuery : GetQuery, IRequest<ListResult<SurveyAnswerListItemDto>>
    {
        public int? SurveyInstanceId { get; set; }
    }

    public class SurveyAnswersQueryHandler : BaseQueryHandler, IRequestHandler<SurveyAnswersQuery, ListResult<SurveyAnswerListItemDto>>
    {
        public SurveyAnswersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<SurveyAnswerListItemDto>> Handle(SurveyAnswersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.SurveyAnswers.Where(x => x.SurveyInstanceId == (request.SurveyInstanceId ?? x.SurveyInstanceId))
                                                                    .OrderBy(x => x.SurveyQuestion.DisplayOrder)
                                                                    .ProjectTo<SurveyAnswerListItemDto>(_mapper.ConfigurationProvider)
                                                                    .ToListAsync(cancellationToken);

            return new ListResult<SurveyAnswerListItemDto>(entities);
        }
    }
}
