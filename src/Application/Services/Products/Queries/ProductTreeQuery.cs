using Engage.Application.Services.ProductMasters.Queries;

namespace Engage.Application.Services.Products.Queries;

public class ProductTreeQuery : PaginatedQuery, IRequest<ListResult<ProductsTreeDto>>
{
}
public record ProductTreeHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductTreeQuery, ListResult<ProductsTreeDto>>
{
    public async Task<ListResult<ProductsTreeDto>> Handle(ProductTreeQuery query, CancellationToken cancellationToken)
    {
        // Level 0 - Product Master
        if (query.GroupKeys.Count == 0)
        {
            var props = ProductMasterPaginationProps.Create();

            var productMasters = await Context.ProductMasters.AsQueryable()
                                                             .AsNoTracking()
                                                             .Filter(query, props)
                                                             .Sort(query, props)
                                                             .SkipQuery(query)
                                                             .TakeQuery(query)
                                                             .ProjectTo<ProductMasterDto>(Mapper.ConfigurationProvider)
                                                             .ToListAsync(cancellationToken);

            var productsTree = productMasters.Select(e => new ProductsTreeDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                ProductSubCategoryName = e.ProductSubCategoryName,
                IsParent = true,
                Code = e.Code,
                ProductBrandName = e.ProductBrandName,
                ProductManufacturerName = e.ProductManufacturerName,
                ProductVendorName = e.ProductVendorName,
                ProductMasterStatusName = e.ProductMasterStatusName,
                ProductMasterSystemStatusName = e.ProductMasterSystemStatusName,
                ProductReasonName = e.ProductReasonName,
                Deleted = e.Deleted,
                Disabled = e.Disabled,
                Files = e.Files,
            }).ToList();

            return new(productsTree);
        }

        // Level 1 - Product
        if (query.GroupKeys.Count == 1)
        {
            var masterProductId = int.Parse(query.GroupKeys[0]);
            var products = await Context.Products.IgnoreQueryFilters()
                                                  .Where(e => e.ProductMasterId == masterProductId)
                                                  .ProjectTo<ProductDto>(Mapper.ConfigurationProvider)
                                                  .ToListAsync(cancellationToken);

            var productMaster = await Context.ProductMasters.AsNoTracking()
                                                            .Where(e => e.ProductMasterId == masterProductId)
                                                            .ProjectTo<ProductMasterDto>(Mapper.ConfigurationProvider)
                                                            .FirstOrDefaultAsync(cancellationToken);

            var productTree = products.Select(e => new ProductsTreeDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                IsParent = false,
                Code = e.Code,
                Disabled = e.Disabled,
                Deleted = e.Deleted,
                ProductSubCategoryName = e.ProductSubCategoryName,
                Files = e.Files,
                //product master
                ProductBrandName = productMaster.ProductBrandName,
                ProductManufacturerName = productMaster.ProductManufacturerName,
                ProductVendorName = productMaster.ProductVendorName,
                ProductMasterStatusName = productMaster.ProductMasterStatusName,
                ProductMasterSystemStatusName = productMaster.ProductMasterSystemStatusName,
                ProductReasonName = productMaster.ProductReasonName,
            }).ToList();

            return new(productTree);
        }

        throw new ArgumentException("Invalid GroupKeys");
    }


}

