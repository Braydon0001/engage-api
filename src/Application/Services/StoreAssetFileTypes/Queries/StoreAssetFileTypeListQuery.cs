namespace Engage.Application.Services.StoreAssetFileTypes.Queries;

public class StoreAssetFileTypeListQuery : IRequest<List<StoreAssetFileTypeDto>>
{

}

public record StoreAssetFileTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetFileTypeListQuery, List<StoreAssetFileTypeDto>>
{
    public async Task<List<StoreAssetFileTypeDto>> Handle(StoreAssetFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetFileTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.StoreAssetFileTypeId)
                              .ProjectTo<StoreAssetFileTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}