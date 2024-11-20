namespace Engage.Application.Services.StoreAssets.Queries;

public class StoreAssetsOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int StoreId { get; set; }
}

public class StoreAssetsOptionsQueryHandler : BaseQueryHandler, IRequestHandler<StoreAssetsOptionsQuery, List<OptionDto>>
{
    public StoreAssetsOptionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OptionDto>> Handle(StoreAssetsOptionsQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.StoreAssets.Where(e => e.StoreId == request.StoreId && e.StoreAssetStatusId != (int)StoreAssetStatusId.Inactive)
                                                 .OrderBy(e => e.Name)
                                                 .Select(e => new OptionDto(e.StoreAssetId, $"{e.StoreAssetType.Name} - {e.StoreAssetSubType.Name}"))
                                                 .ToListAsync(cancellationToken);

        return new List<OptionDto>(entities);
    }
}