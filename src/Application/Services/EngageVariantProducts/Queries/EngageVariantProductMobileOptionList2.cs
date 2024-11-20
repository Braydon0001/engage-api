namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductMobileOptionList2Query : IRequest<List<OptionDto>>
{
    public string Search { get; set; }
    public List<int> DistributionCenterIds { get; set; }
    public List<int> BrandIds { get; set; }
    public List<int> DepartmentIds { get; set; }
    public List<int> SupplierIds { get; set; }
    public List<int> ProductClassificationIds { get; set; }
    public List<int> SubCategoryIds { get; set; }

}

public class EngageVariantProductMobileOptionList2QueryHandler : BaseQueryHandler, IRequestHandler<EngageVariantProductMobileOptionList2Query, List<OptionDto>>
{
    private readonly OrderDefaultsOptions _options;

    public EngageVariantProductMobileOptionList2QueryHandler(IAppDbContext context, IMapper mapper, IOptions<OrderDefaultsOptions> options) : base(context, mapper)
    {
        _options = options.Value;
    }

    public async Task<List<OptionDto>> Handle(EngageVariantProductMobileOptionList2Query query, CancellationToken cancellationToken)
    {
        ValidateQuery(query);
        var dcProductQuery = _context.DCProducts
                            .Include(d => d.EngageVariantProduct)
                                .ThenInclude(v => v.EngageMasterProduct)
                            .Where(e => string.IsNullOrEmpty(e.EngageVariantProduct.Name) == false &&
                                    e.EngageVariantProduct.EngageMasterProduct.Disabled == false && e.EngageVariantProduct.EngageMasterProduct.Deleted == false &&
                                    e.EngageVariantProduct.Disabled == false && e.EngageVariantProduct.Deleted == false &&
                                    e.Disabled == false && e.Deleted == false &&
                                    e.ProductActiveStatusId == _options.ProductActiveStatusId &&
                                    query.DistributionCenterIds.Contains(e.DistributionCenterId))
                            .AsQueryable();

        if (query.BrandIds != null && query.BrandIds.Any())
        {
            dcProductQuery = dcProductQuery.Where(e => query.BrandIds.Contains(e.EngageVariantProduct.EngageMasterProduct.EngageBrandId));
        }

        if (query.DepartmentIds != null && query.DepartmentIds.Any())
        {
            dcProductQuery = dcProductQuery.Where(e => query.DepartmentIds.Contains(e.EngageVariantProduct.EngageMasterProduct.EngageDepartmentId));
        }

        if (query.SupplierIds != null && query.SupplierIds.Any())
        {
            dcProductQuery = dcProductQuery.Where(e => query.SupplierIds.Contains(e.EngageVariantProduct.EngageMasterProduct.SupplierId));
        }

        if (query.ProductClassificationIds != null && query.ProductClassificationIds.Any())
        {
            dcProductQuery = dcProductQuery.Where(e => query.ProductClassificationIds.Contains(e.EngageVariantProduct.EngageMasterProduct.ProductClassificationId));
        }

        if (query.SubCategoryIds != null && query.SubCategoryIds.Any())
        {
            dcProductQuery = dcProductQuery.Where(e => query.SubCategoryIds.Contains(e.EngageVariantProduct.EngageMasterProduct.EngageSubCategoryId));
        }

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            dcProductQuery = dcProductQuery.Where(e => EF.Functions.Like(e.EngageVariantProduct.Code, $"%{query.Search}%") ||
                                                 EF.Functions.Like(e.EngageVariantProduct.Name, $"%{query.Search}%") ||
                                                 EF.Functions.Like(e.Code, $"%{query.Search}%") ||
                                                 EF.Functions.Like(e.Name, $"%{query.Search}%") ||
                                                 EF.Functions.Like(e.EngageVariantProduct.UnitBarcode, $"%{query.Search}%"));
        }



        var list = await dcProductQuery.Select(e => new OptionDto
        {
            Id = (int)e.EngageVariantProductId,
            Name = e.Name + " / " + e.Code + " / " + e.Size + " " + e.UnitType.Name + " / " + e.SubWarehouse + " / " + e.DistributionCenterId + " / " + e.PackSize
        })
                                                .Take(100) // User needs to enter more characters if there are too many options. 
                                                .OrderBy(e => e.Name)
                                                .ToListAsync(cancellationToken);

        var products = list.Where(i => string.IsNullOrEmpty(i.Name) == false);

        return products.ToList();
    }

    private static void ValidateQuery(EngageVariantProductMobileOptionList2Query query)
    {
        query.ThrowIfNull(nameof(query));
        if (query.DistributionCenterIds == null || !query.DistributionCenterIds.Any())
        {
            throw new ArgumentException();
        }

    }
}
