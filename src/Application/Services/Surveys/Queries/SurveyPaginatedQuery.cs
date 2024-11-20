using Engage.Application.Services.Surveys.Models;

namespace Engage.Application.Services.Surveys.Queries;

public class SurveyPaginatedQuery : PaginatedQuery, IRequest<ListResult<SurveyListDto>>
{
}

public record SurveyPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyPaginatedQuery, ListResult<SurveyListDto>>
{
    public async Task<ListResult<SurveyListDto>> Handle(SurveyPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = SurveyPaginationProps.Create();

        var queryable = Context.Surveys.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.SurveyId);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<SurveyListDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}
