using Engage.Application.Services.EngageVariantProducts.Models;

namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductCatalogPaginatedQuery : PaginatedQuery, IRequest<List<EngageVariantProductCatalogDto>>
{
    public string Search { get; set; }
    public int? EngageMasterProductId { get; set; }
    public int? EngageGroupId { get; set; }
    public List<int> EngageSubGroupIds { get; set; }
    public int? EngageCategoryId { get; set; }
    public List<int> EngageSubCategoryIds { get; set; }
    public List<int> EngageBrandIds { get; set; }
    public int? SupplierId { get; set; }
}

public record EngageVariantProductCatalogPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageVariantProductCatalogPaginatedQuery, List<EngageVariantProductCatalogDto>>
{
    public async Task<List<EngageVariantProductCatalogDto>> Handle(EngageVariantProductCatalogPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = EngageVariantProductPaginationProps.Create();

        var queryable = Context.EngageVariantProducts.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.Disabled == false);

        if (query.EngageGroupId.HasValue)
        {
            var subCategoryIds = await Context.EngageSubCategories
                .Where(e => e.EngageCategory.EngageSubGroup.EngageGroup.Id == query.EngageGroupId.Value)
                .Select(e => e.Id)
                .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => subCategoryIds.Contains(e.EngageMasterProduct.EngageSubCategory.Id));
        }

        if (query.EngageSubGroupIds.Any())
        {
            var subCategoryIds = await Context.EngageSubCategories
                .Where(e => query.EngageSubGroupIds
                .Contains(e.EngageCategory.EngageSubGroup.Id))
                .Select(e => e.Id)
                .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => subCategoryIds.Contains(e.EngageMasterProduct.EngageSubCategory.Id));
        }

        if (query.EngageCategoryId.HasValue)
        {
            var subCategoryIds = await Context.EngageSubCategories
                .Where(e => e.EngageCategory.Id == query.EngageCategoryId.Value)
                .Select(e => e.Id)
                .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => subCategoryIds.Contains(e.EngageMasterProduct.EngageSubCategory.Id));
        }

        if (query.EngageSubCategoryIds.Any())
        {
            queryable = queryable.Where(e => query.EngageSubCategoryIds.Contains(e.EngageMasterProduct.EngageSubCategory.Id));
        }

        if (query.EngageBrandIds.Any())
        {
            queryable = queryable.Where(e => query.EngageBrandIds.Contains(e.EngageMasterProduct.EngageBrand.Id));
        }

        if (query.SupplierId.HasValue)
        {
            queryable = queryable.Where(e => e.EngageMasterProduct.SupplierId == query.SupplierId.Value);
        }

        if (query.EngageMasterProductId.HasValue)
        {
            queryable = queryable.Where(e => e.EngageMasterProductId == query.EngageMasterProductId);
        }

        if (!query.Search.IsNullOrEmpty())
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.Name, $"%{query.Search}%"))
                                                    || (EF.Functions.Like(e.UnitBarcode, $"%{query.Search}%"))
                                                    || (EF.Functions.Like(e.Code, $"%{query.Search}%"))
                                                    || (EF.Functions.Like(e.EANNumber, $"%{query.Search}%"))
                                                    || (EF.Functions.Like(e.ShrinkBarcode, $"%{query.Search}%"))
                                                    || (EF.Functions.Like(e.CaseBarcode, $"%{query.Search}%"))
                                                    );
        }

        #region Custom Sort 
        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.Name);
        }
        #endregion

        return await queryable.Paginate(query, props)
                              .ProjectTo<EngageVariantProductCatalogDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


