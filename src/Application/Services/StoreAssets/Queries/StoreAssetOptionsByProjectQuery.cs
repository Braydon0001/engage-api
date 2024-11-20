namespace Engage.Application.Services.StoreAssets.Queries;

public class StoreAssetOptionsByProjectQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public class StoreAssetOptionsByProjectQueryHandler : BaseQueryHandler, IRequestHandler<StoreAssetOptionsByProjectQuery, List<OptionDto>>
{
    public StoreAssetOptionsByProjectQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OptionDto>> Handle(StoreAssetOptionsByProjectQuery request, CancellationToken cancellationToken)
    {
        if (request.ProjectId == 0)
        {
            return [];
        }

        var queryable = _context.StoreAssets.Where(e => e.StoreAssetStatusId != (int)StoreAssetStatusId.Inactive).AsQueryable();

        var projectStoreId = await _context.ProjectStores.Include(e => e.Store)
                                                         .Where(e => e.ProjectId == request.ProjectId)
                                                         .Select(e => e.StoreId)
                                                         .FirstOrDefaultAsync(cancellationToken);

        var tagStoreIds = await _context.ProjectProjectTagStores.Include(e => e.Store)
                                                                .Where(e => e.ProjectId == request.ProjectId)
                                                                .Select(e => e.StoreId)
                                                                .ToListAsync(cancellationToken);

        var storeIds = new List<int> { projectStoreId };
        storeIds.AddRange(tagStoreIds);

        if (storeIds.Count > 0)
        {
            queryable = queryable.Where(e => storeIds.Contains(e.StoreId));
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%")
                                                || EF.Functions.Like(e.StoreAssetSubType.Name, $"%{request.Search}%")
                                                || EF.Functions.Like(e.StoreAssetSubType.StoreAssetType.Name, $"%{request.Search}%")
                                                || EF.Functions.Like(e.StoreAssetType.Name, $"%{request.Search}%")
                                                || EF.Functions.Like(e.StoreAssetOwner.Name, $"%{request.Search}%"));
        }


        var entities = await queryable.OrderBy(e => e.Name)
                                      .Select(e => new OptionDto(e.StoreAssetId, e.StoreAssetType.Name + " - " + e.SerialNumber + " : " + e.Store.Name))
                                      .ToListAsync(cancellationToken);

        return new List<OptionDto>(entities);
    }
}