namespace Engage.Application.Services.SurveyFormAnswerHistories.Queries;

public class SurveyFormAnswerHistoryListQuery : IRequest<List<SurveyFormAnswerHistoryDto>>
{
    public int? SurveyFormAnswerId { get; set; }
}

public record SurveyFormAnswerHistoryListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormAnswerHistoryListQuery, List<SurveyFormAnswerHistoryDto>>
{
    public async Task<List<SurveyFormAnswerHistoryDto>> Handle(SurveyFormAnswerHistoryListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormAnswerHistories.AsQueryable().AsNoTracking();

        if (query.SurveyFormAnswerId.HasValue)
        {
            queryable = queryable.Where(e => e.SurveyFormAnswerId == query.SurveyFormAnswerId);
        }

        return await queryable.OrderBy(e => e.SurveyFormAnswerHistoryId)
                              .ProjectTo<SurveyFormAnswerHistoryDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}