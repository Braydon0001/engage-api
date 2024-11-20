namespace Engage.Application.Services.StoreAssetStatuses.Queries;

public class StoreAssetStatusOptionQuery : IRequest<List<StoreAssetStatusOption>>
{ 

}

public record StoreAssetStatusOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetStatusOptionQuery, List<StoreAssetStatusOption>>
{
    public async Task<List<StoreAssetStatusOption>> Handle(StoreAssetStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.StoreAssetStatusId)
                              .ProjectTo<StoreAssetStatusOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}