// auto-generated
namespace Engage.Application.Services.Products.Queries;

public class ProductPaginatedOptionListQuery : PaginatedQuery, IRequest<List<ProductOption>>
{

}

public class ProductPaginatedOptionListHandler : ListQueryHandler, IRequestHandler<ProductPaginatedOptionListQuery, List<ProductOption>>
{
    public ProductPaginatedOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductOption>> Handle(ProductPaginatedOptionListQuery query, CancellationToken cancellationToken)
    {
        var paginationModels = CreatePaginationModels();

        var queryable = _context.Products.AsQueryable().AsNoTracking();

        var entities = await queryable.Filter(query, paginationModels)
                                      .Sort(query, paginationModels)
                                      .Skip(query.StartRow)
                                      .Take(query.PageSize)
                                      .ProjectTo<ProductOption>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

         return entities;                             
    }
 
    private static Dictionary<string, PaginationProperty> CreatePaginationModels()
    {
        return new Dictionary<string, PaginationProperty> {

            { "id", new PaginationProperty("productId") },
            { "productMasterName", new PaginationProperty("ProductMaster.Name") },
            { "relatedProductName", new PaginationProperty("Product.Name") },
            { "productWarehouseName", new PaginationProperty("ProductWarehouse.Name") },
            { "productSizeTypeName", new PaginationProperty("ProductSizeType.Name") },
            { "productPackSizeTypeName", new PaginationProperty("ProductPackSizeType.Name") },
            { "productModuleStatusName", new PaginationProperty("ProductModuleStatus.Name") },
            { "productMasterColorName", new PaginationProperty("productMasterColor.Name") },
            { "productMasterSizeName", new PaginationProperty("productMasterSize.Name") },
            { "code", new PaginationProperty("undefined") },
            { "description", new PaginationProperty("undefined") },
            { "productSize", new PaginationProperty("ProductSize") },
            { "productPackSize", new PaginationProperty("ProductPackSize") },
            { "files", new PaginationProperty("Files") }    

        };
    }
}


