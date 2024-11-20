// auto-generated
namespace Engage.Application.Services.Products.Queries;

public class ProductPaginatedQuery : PaginatedQuery, IRequest<ListResult<ProductDto>>
{
}

public class ProductPaginatedListHandler : ListQueryHandler, IRequestHandler<ProductPaginatedQuery, ListResult<ProductDto>>
{
    public ProductPaginatedListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ProductDto>> Handle(ProductPaginatedQuery query, CancellationToken cancellationToken)
    {
        var paginationModels = CreatePaginationProps();

        var queryable = _context.Products.AsQueryable().AsNoTracking();

        var entities = await queryable.Filter(query, paginationModels)
                                      .Sort(query, paginationModels)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<ProductDto>(entities);
    }

    private static Dictionary<string, PaginationProperty> CreatePaginationProps()
    {
        return new Dictionary<string, PaginationProperty> {

            { "id", new PaginationProperty("ProductId") },
            { "productMasterName", new PaginationProperty("ProductMaster.Description") },
            { "relatedProductName", new PaginationProperty("RelatedProduct.Description") },
            { "productWarehouseName", new PaginationProperty("ProductWarehouse.Name") },
            { "productSizeTypeName", new PaginationProperty("ProductSizeType.Name") },
            { "productPackSizeTypeName", new PaginationProperty("ProductPackSizeType.Name") },
            { "productModuleStatusName", new PaginationProperty("ProductModuleStatus.Name") },
            { "productSystemStatusName", new PaginationProperty("ProductSystemStatus.Name") },
            { "productMasterColorName", new PaginationProperty("ProductMasterColor.Name") },
            { "productMasterSizeName", new PaginationProperty("ProductMasterSize.Name") },
            { "productSubCategoryName", new PaginationProperty("ProductMaster.ProductSubCategory.Name") },
            { "code", new PaginationProperty("Code") },
            { "description", new PaginationProperty("Description") },
            { "productSize", new PaginationProperty("ProductSize") },
            { "productPackSize", new PaginationProperty("ProductPackSize") },
            { "files", new PaginationProperty("Files") }

        };
    }
}


