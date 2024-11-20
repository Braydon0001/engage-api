namespace Engage.Application.Services.SurveyForms.Queries;

public class SurveyFormPaginatedOptionQuery : PaginatedQuery, IRequest<List<SurveyFormOption>>
{
}

public record SurveyFormPaginatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormPaginatedOptionQuery, List<SurveyFormOption>>
{
    public async Task<List<SurveyFormOption>> Handle(SurveyFormPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = SurveyFormPaginationProps.Create();

        var queryable = Context.SurveyForms.AsQueryable().AsNoTracking();

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<SurveyFormOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }    
}


