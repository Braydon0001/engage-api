namespace Engage.Application.Services.DCProducts.Queries;

public class DCProductOptionsByProjectQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public class DCProductOptionsByProjectQueryHandler : BaseQueryHandler, IRequestHandler<DCProductOptionsByProjectQuery, List<OptionDto>>
{
    private readonly IUserService _user;

    public DCProductOptionsByProjectQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<List<OptionDto>> Handle(DCProductOptionsByProjectQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.DCProducts.Where(e => e.Disabled == false && e.Deleted == false
                                                    && e.EngageVariantProduct.Disabled == false
                                                    && e.EngageVariantProduct.Deleted == false
                                                    && e.EngageVariantProduct.EngageMasterProduct.Disabled == false
                                                    && e.EngageVariantProduct.EngageMasterProduct.Deleted == false)
                                            .AsQueryable();

        //TODO: Hardcoded Accelerate Supplier Escape for Matt's Demo on 19/06/2024 -- REMOVE!
        if (!_user.IsHostSupplier && _user.SupplierId > 0 && _user.SupplierId != 275)
        {
            queryable = queryable.Where(e => e.EngageVariantProduct.EngageMasterProduct.SupplierId == _user.SupplierId);
        }

        var dcIds = new List<int>();
        if (query.ProjectId != 0)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(v => v.ProjectId == query.ProjectId, cancellationToken);
            var projectStore = await _context.ProjectStores.FirstOrDefaultAsync(v => v.ProjectId == query.ProjectId, cancellationToken);

            if (project.EngageRegionId.HasValue)
            {
                var projectDcIds = await _context.DCAccounts.Where(e => e.Store.EngageRegionId == project.EngageRegionId).Select(e => e.DistributionCenterId).ToListAsync(cancellationToken);
                if (projectDcIds.Count > 0)
                {
                    dcIds.AddRange(projectDcIds);
                }
            }

            if (projectStore != null)
            {
                var projectDcIds = await _context.DCAccounts.Where(e => e.StoreId == projectStore.StoreId).Select(e => e.DistributionCenterId).ToListAsync(cancellationToken);
                if (projectDcIds.Count > 0)
                {
                    dcIds.AddRange(projectDcIds);
                }
            }

            var projectStoreIds = await _context.ProjectProjectTagStores.Where(e => e.ProjectId == query.ProjectId).Select(e => e.StoreId).ToListAsync(cancellationToken);
            if (projectStoreIds.Count > 0)
            {
                var projectDcIds = await _context.DCAccounts.Where(e => projectStoreIds.Contains(e.StoreId)).Select(e => e.DistributionCenterId).ToListAsync(cancellationToken);
                if (projectDcIds.Count > 0)
                {
                    dcIds.AddRange(projectDcIds);
                }
            }

            var projectRegionIds = await _context.ProjectProjectTagEngageRegions.Where(e => e.ProjectId == query.ProjectId).Select(e => e.EngageRegionId).ToListAsync(cancellationToken);
            if (projectRegionIds.Count > 0)
            {
                var projectDcIds = await _context.DCAccounts.Where(e => projectRegionIds.Contains(e.Store.EngageRegionId)).Select(e => e.DistributionCenterId).ToListAsync(cancellationToken);
                if (projectDcIds.Count > 0)
                {
                    dcIds.AddRange(projectDcIds);
                }
            }
        }

        if (dcIds.Count > 0)
        {
            dcIds = dcIds.Distinct().ToList();

            queryable = queryable.Where(e => dcIds.Contains(e.DistributionCenterId));
        }

        return await queryable
                    .Where(e => (
                                EF.Functions.Like(e.Code, $"%{query.Search}%") ||
                                EF.Functions.Like(e.Name, $"%{query.Search}%") ||
                                EF.Functions.Like(e.Code, $"%{query.Search}%") ||
                                EF.Functions.Like(e.Name, $"%{query.Search}%")))
                    .Select(e => new OptionDto
                    {
                        Id = e.DCProductId,
                        Name = e.Name
                    })
                    .Take(100)
                    .OrderBy(e => e.Name)
                    .ToListAsync(cancellationToken);
    }
}
