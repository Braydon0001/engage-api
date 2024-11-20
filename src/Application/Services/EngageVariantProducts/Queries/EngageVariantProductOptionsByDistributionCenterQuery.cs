namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductOptionsByDistributionCenterQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? DistributionCenterId { get; set; }
}

public class EngageVariantProductOptionsByDistributionCenterQueryHandler : BaseQueryHandler, IRequestHandler<EngageVariantProductOptionsByDistributionCenterQuery, List<OptionDto>>
{
    private readonly IUserService _user;

    public EngageVariantProductOptionsByDistributionCenterQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<List<OptionDto>> Handle(EngageVariantProductOptionsByDistributionCenterQuery query, CancellationToken cancellationToken)
    {
        ValidateQuery(query);

        var queryable = _context.EngageVariantProducts.AsQueryable();

        if (!_user.IsHostSupplier && _user.SupplierId > 0)
        {
            queryable = queryable.Where(e => e.EngageMasterProduct.SupplierId == _user.SupplierId);
        }

        return await queryable
                        .Join(_context.DCProducts,
                              variantProduct => variantProduct.EngageVariantProductId,
                              dcProduct => dcProduct.EngageVariantProductId,
                              (variantProduct, dcProduct) => new { variantProduct, dcProduct })
                        .Where(e => e.dcProduct.DistributionCenterId == query.DistributionCenterId && (
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

    private static void ValidateQuery(EngageVariantProductOptionsByDistributionCenterQuery query)
    {
        query.ThrowIfNull(nameof(query));
        if (query.DistributionCenterId < 0)
        {
            throw new ArgumentException();
        }
        query.Search.ThrowIfNullOrWhiteSpace(nameof(query.Search));
    }
}
