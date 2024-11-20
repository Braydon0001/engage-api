namespace Engage.Application.Services.ApiKeys.Queries;

public class ApiKeyListQuery : IRequest<List<ApiKeyDto>>
{

}

public record ApiKeyListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ApiKeyListQuery, List<ApiKeyDto>>
{
    public async Task<List<ApiKeyDto>> Handle(ApiKeyListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ApiKeys.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ApiKeyDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}