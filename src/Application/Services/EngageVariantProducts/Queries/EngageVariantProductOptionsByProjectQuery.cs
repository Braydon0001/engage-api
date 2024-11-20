namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductOptionsByProjectQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public class EngageVariantProductOptionsByProjectQueryHandler : BaseQueryHandler, IRequestHandler<EngageVariantProductOptionsByProjectQuery, List<OptionDto>>
{
    private readonly IUserService _user;

    public EngageVariantProductOptionsByProjectQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<List<OptionDto>> Handle(EngageVariantProductOptionsByProjectQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EngageVariantProducts.Where(e => e.Disabled == false && e.Deleted == false).AsQueryable();

        if (!_user.IsHostSupplier && _user.SupplierId > 0)
        {
            queryable = queryable.Where(e => e.EngageMasterProduct.SupplierId == _user.SupplierId);
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
        }

        if (dcIds.Count > 0)
        {
            dcIds = dcIds.Distinct().ToList();
        }

        return await queryable
                    .Join(_context.DCProducts,
                          variantProduct => variantProduct.EngageVariantProductId,
                          dcProduct => dcProduct.EngageVariantProductId,
                          (variantProduct, dcProduct) => new { variantProduct, dcProduct })
                    .Where(e => dcIds.Contains(e.dcProduct.DistributionCenterId) && e.dcProduct.Deleted == false
                                && e.dcProduct.Disabled == false && (
                                EF.Functions.Like(e.variantProduct.Code, $"%{query.Search}%") ||
                                EF.Functions.Like(e.variantProduct.Name, $"%{query.Search}%") ||
                                EF.Functions.Like(e.dcProduct.Code, $"%{query.Search}%") ||
                                EF.Functions.Like(e.dcProduct.Name, $"%{query.Search}%")))
                    .Select(e => new OptionDto
                    {
                        Id = e.variantProduct.EngageVariantProductId,
                        Name = e.dcProduct.Code + " / " + e.variantProduct.Name + " / " + e.dcProduct.Size + " " + e.dcProduct.UnitType.Name
                    })
                    .Take(100)
                    .OrderBy(e => e.Name)
                    .ToListAsync(cancellationToken);
    }
}
