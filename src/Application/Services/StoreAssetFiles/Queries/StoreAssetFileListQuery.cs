namespace Engage.Application.Services.StoreAssetFiles.Queries;

public class StoreAssetFileListQuery : IRequest<List<StoreAssetFileDto>>
{
    public int StoreAssetId { get; set; }
}

public record StoreAssetFileListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetFileListQuery, List<StoreAssetFileDto>>
{
    public async Task<List<StoreAssetFileDto>> Handle(StoreAssetFileListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetFiles.AsQueryable().AsNoTracking();

        if (query.StoreAssetId > 0)
        {
            queryable = queryable.Where(e => e.StoreAssetId == query.StoreAssetId);
        }

        return await queryable.OrderByDescending(e => e.Created)
                              .ProjectTo<StoreAssetFileDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}