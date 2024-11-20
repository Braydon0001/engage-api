namespace Engage.Application.Services.StoreAssetFileTypes.Queries;

public class StoreAssetFileTypeOptionQuery : IRequest<List<StoreAssetFileTypeOption>>
{ 

}

public record StoreAssetFileTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetFileTypeOptionQuery, List<StoreAssetFileTypeOption>>
{
    public async Task<List<StoreAssetFileTypeOption>> Handle(StoreAssetFileTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetFileTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.StoreAssetFileTypeId)
                              .ProjectTo<StoreAssetFileTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}