using Engage.Application.Services.Products.Models;

namespace Engage.Application.Services.Products.Queries;

public class ProductsQuery : GetQuery, IRequest<List<ProductOptionDto>>
{
    public int DistributionCenterId { get; set; }
    public bool IsSupplierProductsOnly { get; set; } = false;
}

public class OrderSkuProductsQueryHandler : IRequestHandler<ProductsQuery, List<ProductOptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public OrderSkuProductsQueryHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<List<ProductOptionDto>> Handle(ProductsQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EngageVariantProducts.Where(e => e.Disabled == false && e.EngageMasterProduct.Disabled == false);

        // For a tenant supplier
        if (!_user.IsHostSupplier)
        {
            var useSupplierProducts = await UseSupplierProducts(query, cancellationToken);


            // Filter the products by SupplierProducts 
            if (useSupplierProducts)
            {
                var allowedMasterProductIds = await _context.SupplierProducts.Where(e => e.SupplierId == _user.SupplierId)
                                                                             .Select(e => e.EngageMasterProductId)
                                                                             .ToListAsync(cancellationToken);
                queryable = queryable.Where(e => allowedMasterProductIds.Contains(e.EngageMasterProductId));
            }
            // OR

            // Filter the products by the supplier
            else
            {
                queryable = queryable.Where(e => e.EngageMasterProduct.SupplierId == _user.SupplierId);
            }
        }

        var search = $"%{query.Search}%";

        return await queryable.Join(_context.DCProducts.Where(e => e.Disabled == false && e.EngageVariantProduct.Disabled == false && e.EngageVariantProduct.EngageMasterProduct.Disabled == false && e.ProductActiveStatusId == 1),
                                    variantProduct => variantProduct.EngageVariantProductId,
                                    dcProduct => dcProduct.EngageVariantProductId,
                                    (variantProduct, dcProduct) => new { variantProduct, dcProduct })
                              .Where(e => e.dcProduct.DistributionCenterId == query.DistributionCenterId && (
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
    }

    //TODO Settings
    private async Task<bool> UseSupplierProducts(ProductsQuery query, CancellationToken cancellationToken)
    {
        if (query.IsSupplierProductsOnly)
        {
            var suppressUseSupplierProducts = await _context.Users.Where(e => e.Email == _user.UserName).Select(e => e.IgnoreOrderProductFilters).SingleOrDefaultAsync(cancellationToken);
            if (!suppressUseSupplierProducts)
            {
                return await _context.Suppliers.Where(e => e.SupplierId == _user.SupplierId).Select(e => e.IsSupplierProductsOnly).SingleAsync(cancellationToken);
            }
        }
        return false;
    }
}
