using Engage.Application.Services.Products.Models;

namespace Engage.Application.Services.Products.Queries;

public class ClaimProductsQuery : GetQuery, IRequest<List<ProductOptionDto>>
{
    public int DistributionCenterId { get; set; }
    public int SupplierId { get; set; }
}

public class ClaimSkuProductsQueryHandler : IRequestHandler<ClaimProductsQuery, List<ProductOptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public ClaimSkuProductsQueryHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<List<ProductOptionDto>> Handle(ClaimProductsQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EngageVariantProducts.Where(e => e.Disabled == false && e.EngageMasterProduct.Disabled == false);
        var search = $"%{query.Search}%";

        var products = await queryable.Join(_context.DCProducts.Where(e => e.Disabled == false &&
                                                                           e.ProductActiveStatusId == 1),
                                            variantProduct => variantProduct.EngageVariantProductId,
                                            dcProduct => dcProduct.EngageVariantProductId,
                                            (variantProduct, dcProduct) => new { variantProduct, dcProduct })
                                       .Where(e => e.dcProduct.DistributionCenterId == query.DistributionCenterId 
                                                   && (e.dcProduct.EngageVariantProduct.EngageMasterProduct.SupplierId == query.SupplierId 
                                                   || e.dcProduct.EngageVariantProduct.EngageMasterProduct.IsAllSuppliersProduct == true) && (
                                                   EF.Functions.Like(e.variantProduct.Code, search) ||
                                                   EF.Functions.Like(e.variantProduct.Name, search) ||
                                                   EF.Functions.Like(e.dcProduct.Code, search) ||
                                                   EF.Functions.Like(e.dcProduct.Name, search)))
                                       .Select(e => new ProductOptionDto
                                       {
                                           EngageMasterProductId = e.variantProduct.EngageMasterProductId,
                                           Id = e.variantProduct.EngageVariantProductId,
                                           DCProductId = e.dcProduct.DCProductId,
                                           Name = e.dcProduct.Code + " / " + e.variantProduct.Name + " / " + e.dcProduct.Size + " " + e.dcProduct.UnitType.Name
                                       })
                                       .Take(100)
                                       .OrderBy(e => e.Name)
                                       .ToListAsync(cancellationToken);

        return products;
    }
}
