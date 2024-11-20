using Engage.Application.Services.SurveyQuestionOptions.Models;

namespace Engage.Application.Services.SurveyQuestionOptions.Queries;

public class SurveyQuestionOptionsQuery : GetQuery, IRequest<ListResult<SurveyQuestionOptionListItemDto>>
{
    public int? SurveyQuestionId { get; set; }
}

public class SurveyQuestionOptionsQueryHandler : BaseQueryHandler, IRequestHandler<SurveyQuestionOptionsQuery, ListResult<SurveyQuestionOptionListItemDto>>
{
    public SurveyQuestionOptionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<SurveyQuestionOptionListItemDto>> Handle(SurveyQuestionOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.SurveyQuestionOptions.Where(e => e.SurveyQuestionId == (request.SurveyQuestionId ?? e.SurveyQuestionId))
                                                           .OrderBy(e => e.DisplayOrder)
                                                           .ProjectTo<SurveyQuestionOptionListItemDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

        return new ListResult<SurveyQuestionOptionListItemDto>(entities);
    }
}
