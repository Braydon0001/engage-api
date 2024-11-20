namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductMobileOptionListQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? DistributionCenterId { get; set; }
}

public class EngageVariantProductMobileOptionListQueryHandler : BaseQueryHandler, IRequestHandler<EngageVariantProductMobileOptionListQuery, List<OptionDto>>
{
    private readonly OrderDefaultsOptions _options;

    public EngageVariantProductMobileOptionListQueryHandler(IAppDbContext context, IMapper mapper, IOptions<OrderDefaultsOptions> options) : base(context, mapper)
    {
        _options = options.Value;
    }

    public async Task<List<OptionDto>> Handle(EngageVariantProductMobileOptionListQuery query, CancellationToken cancellationToken)
    {
        ValidateQuery(query);

        var list = await _context.EngageVariantProducts
                        .Join(_context.DCProducts,
                              variantProduct => variantProduct.EngageVariantProductId,
                              dcProduct => dcProduct.EngageVariantProductId,
                              (variantProduct, dcProduct) => new { variantProduct, dcProduct })
                        .Where(e => e.dcProduct.DistributionCenterId == query.DistributionCenterId &&
                                    string.IsNullOrEmpty(e.variantProduct.Name) == false &&
                                    e.variantProduct.Disabled == false &&
                                    e.dcProduct.Disabled == false && e.dcProduct.ProductActiveStatusId == _options.ProductActiveStatusId && (
                                    EF.Functions.Like(e.variantProduct.Code, $"%{query.Search}%") ||
                                    EF.Functions.Like(e.variantProduct.Name, $"%{query.Search}%") ||
                                    EF.Functions.Like(e.dcProduct.Code, $"%{query.Search}%") ||
                                    EF.Functions.Like(e.dcProduct.Name, $"%{query.Search}%") ||
                                    EF.Functions.Like(e.variantProduct.UnitBarcode, $"%{query.Search}%"))
                                    )
                        .Select(e => new OptionDto
                        {
                            Id = e.variantProduct.EngageVariantProductId,
                            Name = e.variantProduct.Name + " / " + e.dcProduct.Code + " / " + e.dcProduct.Size + " " + e.dcProduct.UnitType.Name + " / " + e.dcProduct.SubWarehouse + " / " + e.dcProduct.DistributionCenterId
                        })
                        .Take(100) // User needs to enter more characters if there are too many options. 
                        .OrderBy(e => e.Name)
                        .ToListAsync(cancellationToken);

        var products = list.Where(i => string.IsNullOrEmpty(i.Name) == false);

        return products.ToList();
    }

    private static void ValidateQuery(EngageVariantProductMobileOptionListQuery query)
    {
        query.ThrowIfNull(nameof(query));
        if (query.DistributionCenterId < 0)
        {
            throw new ArgumentException();
        }
        query.Search.ThrowIfNullOrWhiteSpace(nameof(query.Search));
    }
}
