namespace Engage.Application.Services.StoreAssetStatuses.Queries;

public class StoreAssetStatusListQuery : IRequest<List<StoreAssetStatusDto>>
{

}

public record StoreAssetStatusListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetStatusListQuery, List<StoreAssetStatusDto>>
{
    public async Task<List<StoreAssetStatusDto>> Handle(StoreAssetStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.StoreAssetStatusId)
                              .ProjectTo<StoreAssetStatusDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}