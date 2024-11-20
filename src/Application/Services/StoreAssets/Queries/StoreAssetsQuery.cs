using Engage.Application.Services.StoreAssets.Models;

namespace Engage.Application.Services.StoreAssets.Queries;

public class StoreAssetsQuery : GetQuery, IRequest<ListResult<StoreAssetDto>>
{
    public int StoreId { get; set; }
}

public class StoreAssetsQueryHandler : BaseQueryHandler, IRequestHandler<StoreAssetsQuery, ListResult<StoreAssetDto>>
{
    public StoreAssetsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StoreAssetDto>> Handle(StoreAssetsQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.StoreAssets.Where(e => e.StoreId == request.StoreId)
                                                 .OrderBy(e => e.Name)
                                                 .ProjectTo<StoreAssetDto>(_mapper.ConfigurationProvider)
                                                 .ToListAsync(cancellationToken);

        var files = await _context.StoreAssetFiles.AsNoTracking()
                                                  .Where(e => entities.Select(e => e.Id).Contains(e.StoreAssetId))
                                                  .ToListAsync(cancellationToken);

        foreach (var asset in entities)
        {
            var fileList = files.Where(e => e.StoreAssetId == asset.Id).Select(e => e.Files).ToList();
            asset.Files = JoinLists(fileList);
        }

        return new ListResult<StoreAssetDto>(entities.OrderBy(e => e.Id).ToList());
    }

    private static List<JsonFile> JoinLists(List<List<JsonFile>> fileList)
    {
        List<JsonFile> returnList = new();
        foreach (var file in fileList)
        {
            returnList.AddRange(file);
        }

        return returnList;
    }
}
