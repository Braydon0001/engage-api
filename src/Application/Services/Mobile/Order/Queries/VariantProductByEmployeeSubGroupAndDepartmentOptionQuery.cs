namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class VariantProductByEmployeeSubGroupAndDepartmentOptionQuery : IRequest<List<OptionDto>>
{
    public int EmployeeId { get; set; }
}

public class VariantProductByEmployeeSubGroupAndDepartmentOptionQueryHandler : BaseQueryHandler, IRequestHandler<VariantProductByEmployeeSubGroupAndDepartmentOptionQuery, List<OptionDto>>
{
    private readonly OrderDefaultsOptions _options;

    public VariantProductByEmployeeSubGroupAndDepartmentOptionQueryHandler(IAppDbContext context, IMapper mapper, IOptions<OrderDefaultsOptions> options) : base(context, mapper)
    {
        _options = options.Value;
    }

    public async Task<List<OptionDto>> Handle(VariantProductByEmployeeSubGroupAndDepartmentOptionQuery query, CancellationToken cancellationToken)
    {
        var EngageSubGroupIds = await _context.EmployeeStores
                                    .Where(e => e.EmployeeId == query.EmployeeId)
                                    .Select(e => e.EngageSubGroupId)
                                    .ToListAsync(cancellationToken);

        var EmployeeDepartmentIds = await _context.EmployeeDepartments
                                        .Where(e => e.EmployeeId == query.EmployeeId)
                                        .Select(s => s.EngageDepartmentId)
                                        .ToListAsync(cancellationToken);

        var EmployeeRegionIds = await _context.EmployeeRegions
                                        .Where(e => e.EmployeeId == query.EmployeeId)
                                        .Select(s => s.EngageRegionId)
                                        .ToListAsync(cancellationToken);

        var EmployeeRegionStoresDcAccounts = await _context.Stores
                                        .Where(e => EmployeeRegionIds.Distinct().Contains(e.EngageRegionId))
                                        .Select(s => s.DCAccounts)
                                        .ToListAsync(cancellationToken);

        var EmployeeRegionStoresDcIds = new List<int>();

        foreach (var dcAccount in EmployeeRegionStoresDcAccounts)
        {
            EmployeeRegionStoresDcIds.AddRange(dcAccount.Select(e => e.DistributionCenterId));
        }

        EmployeeRegionStoresDcIds = EmployeeRegionStoresDcIds.Distinct().ToList();

        var dcProducts = await _context.DCProducts
                            .Include(d => d.UnitType)
                            .Include(d => d.EngageVariantProduct)
                                .ThenInclude(v => v.EngageMasterProduct)
                                    .ThenInclude(m => m.EngageSubCategory)
                                    .ThenInclude(s => s.EngageCategory)
                                    .ThenInclude(c => c.EngageSubGroup)
                            .Where(d => d.Disabled == false && d.Deleted == false
                                && d.EngageVariantProduct.Disabled == false && d.EngageVariantProduct.Deleted == false
                                && d.EngageVariantProduct.EngageMasterProduct.Disabled == false && d.EngageVariantProduct.EngageMasterProduct.Deleted == false
                                && EngageSubGroupIds.Distinct().Contains(d.EngageVariantProduct.EngageMasterProduct.EngageSubCategory.EngageCategory.EngageSubGroup.Id)
                                && EmployeeDepartmentIds.Distinct().Contains(d.EngageVariantProduct.EngageMasterProduct.EngageSubCategory.EngageCategory.EngageSubGroup.EngageDepartmentId)
                                && EmployeeRegionStoresDcIds.Contains(d.DistributionCenterId)
                                && d.ProductActiveStatusId == _options.ProductActiveStatusId
                                && string.IsNullOrEmpty(d.Code) == false)
                            .ToListAsync(cancellationToken);

        var res = dcProducts
                       .Select(d => new OptionDto
                       {
                           Id = (int)d.EngageVariantProductId,
                           Name = d.Name + " / "
                                    + d.Code + " / "
                                    + d.Size + " "
                                    + d.UnitType.Name + " / "
                                    + d.SubWarehouse + " / "
                                    + d.DistributionCenterId,
                       })
                       .OrderBy(e => e.Name)
                       .ToList();

        return res.ToList();

    }
}
