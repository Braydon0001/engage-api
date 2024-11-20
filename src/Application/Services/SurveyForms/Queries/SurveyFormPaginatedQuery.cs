namespace Engage.Application.Services.SurveyForms.Queries;

public class SurveyFormPaginatedQuery : PaginatedQuery, IRequest<ListResult<SurveyFormDto>>
{
}

public record SurveyFormPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormPaginatedQuery, ListResult<SurveyFormDto>>
{
    public async Task<ListResult<SurveyFormDto>> Handle(SurveyFormPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = SurveyFormPaginationProps.Create();

        var queryable = Context.SurveyForms.AsQueryable().AsNoTracking();

        var entities = await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<SurveyFormDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        return new(entities);
    }
}


