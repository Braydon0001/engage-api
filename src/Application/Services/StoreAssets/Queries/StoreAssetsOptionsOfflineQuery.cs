namespace Engage.Application.Services.StoreAssets.Queries;

public class StoreAssetsOptionsOfflineQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int UserId { get; set; }
}

public class StoreAssetsOptionsOfflineQueryHandler : BaseQueryHandler, IRequestHandler<StoreAssetsOptionsOfflineQuery, List<OptionDto>>
{
    public StoreAssetsOptionsOfflineQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OptionDto>> Handle(StoreAssetsOptionsOfflineQuery query, CancellationToken cancellationToken)
    {
        var userStakeholderIds = await _context.ProjectStakeholderUsers.Where(e => e.UserId == query.UserId && e.Disabled != true).Select(e => e.ProjectStakeholderId).ToListAsync(cancellationToken);





        var projectIds = await _context.ProjectAssignees.AsNoTracking()
                                                       .Where(e => userStakeholderIds.Contains(e.ProjectStakeholderId))
                                                       .Include(e => e.ProjectStakeholder)
                                                       .Select(e => e.ProjectId)
        .ToListAsync(cancellationToken);

        var ownerProjectIds = await _context.ProjectStores.Where(e => e.OwnerId == query.UserId).Select(e => e.ProjectId).ToListAsync(cancellationToken);


        projectIds.AddRange(ownerProjectIds);


        var projectStoreIds = await _context.ProjectStoreAssets.Where(e => projectIds.Contains(e.ProjectId))
                                                 .Select(e => e.StoreAssetId)
                                                 .ToListAsync(cancellationToken);

        var entities = await _context.StoreAssets.Where(e => projectStoreIds.Contains(e.StoreAssetId) && e.StoreAssetStatusId != (int)StoreAssetStatusId.Inactive)
                                                 .OrderBy(e => e.Name)
                                                 .Select(e => new OptionDto(e.StoreAssetId, $"{e.StoreAssetType.Name} - {e.StoreAssetSubType.Name}"))
                                                 .ToListAsync(cancellationToken);

        return new List<OptionDto>(entities);
    }
}