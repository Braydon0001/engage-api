namespace Engage.Application.Services.StoreAssetFiles.Queries;

public class StoreAssetFileOptionQuery : IRequest<List<StoreAssetFileOption>>
{ 

}

public record StoreAssetFileOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetFileOptionQuery, List<StoreAssetFileOption>>
{
    public async Task<List<StoreAssetFileOption>> Handle(StoreAssetFileOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetFiles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.StoreAssetFileId)
                              .ProjectTo<StoreAssetFileOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}