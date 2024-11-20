using Engage.Application.Services.EngageMasterProducts.Models;

namespace Engage.Application.Services.EngageMasterProducts.Queries;

public class EngageMasterProductRelatedProductsQuery : PaginatedQuery, IRequest<List<EngageProductMasterVariantRelatedDto>>
{
    public int SupplierId { get; set; }
    public int EngageBrandId { get; set; }
    public int EngageGroupId { get; set; }
    public int EngageSubGroupId { get; set; }
    public int EngageCategoryId { get; set; }
    public int EngageSubCategoryId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}
public record EngageMasterProductRelatedProductsHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageMasterProductRelatedProductsQuery, List<EngageProductMasterVariantRelatedDto>>
{
    public async Task<List<EngageProductMasterVariantRelatedDto>> Handle(EngageMasterProductRelatedProductsQuery query, CancellationToken cancellationToken)
    {
        if (query.GroupKeys.Count == 0)
        {
            var queryable = Context.EngageMasterProducts.AsNoTracking().AsQueryable();

            if (query.SupplierId > 0)
            {
                queryable = queryable.Where(e => e.SupplierId == query.SupplierId);
            }

            if (query.EngageBrandId > 0)
            {
                queryable = queryable.Where(e => e.EngageBrandId == query.EngageBrandId);
            }

            if (query.EngageSubCategoryId > 0)
            {
                queryable = queryable.Where(e => e.EngageSubCategoryId == query.EngageSubCategoryId);
            }
            else if (query.EngageCategoryId > 0)
            {
                var subCategoryIds = await Context.EngageSubCategories
                                                  .AsNoTracking()
                                                  .Where(e => e.EngageCategoryId == query.EngageCategoryId)
                                                  .Select(e => e.Id)
                                                  .ToListAsync(cancellationToken);
                queryable = queryable.Where(e => subCategoryIds.Contains(e.EngageSubCategoryId));
            }
            else if (query.EngageSubGroupId > 0)
            {
                var subCategoryIds = await Context.EngageSubCategories
                                                  .AsNoTracking()
                                                  .Where(e => e.EngageCategory.EngageSubGroupId == query.EngageSubGroupId)
                                                  .Select(e => e.Id)
                                                  .ToListAsync(cancellationToken);
                queryable = queryable.Where(e => subCategoryIds.Contains(e.EngageSubCategoryId));
            }
            else if (query.EngageGroupId > 0)
            {
                var engageCategoryIds = await Context.EngageCategories
                                                  .AsNoTracking()
                                                  .Where(e => e.EngageSubGroup.EngageGroupId == query.EngageGroupId)
                                                  .Select(e => e.Id)
                                                  .ToListAsync(cancellationToken);
                queryable = queryable.Where(e => engageCategoryIds.Contains(e.EngageSubCategory.EngageCategoryId));
            }

            if (query.Name.IsNotNullOrEmpty())
            {
                queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{query.Name}%"));
            }

            if (query.Code.IsNotNullOrEmpty())
            {
                queryable = queryable.Where(e => EF.Functions.Like(e.Code, $"%{query.Code}%"));
            }

            var entities = await queryable.OrderBy(e => e.EngageMasterProductId)
                                    .SkipQuery(query)
                                    .TakeQuery(query)
                                    .ProjectTo<EngageProductMasterVariantRelatedDto>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

            return entities;
        }
        else if (query.GroupKeys.Count == 1)
        {
            if (int.TryParse(query.GroupKeys[0], out int masterId))
            {
                var variants = await Context.EngageVariantProducts
                                            .AsNoTracking()
                                            .Where(e => e.EngageMasterProductId == masterId)
                                            .ProjectTo<EngageProductMasterVariantRelatedDto>(Mapper.ConfigurationProvider)
                                            .ToListAsync(cancellationToken);

                return variants;
            }
        }
        else if (query.GroupKeys.Count == 2)
        {
            if (int.TryParse(query.GroupKeys[1], out int variantId))
            {
                var dcProducts = await Context.DCProducts
                                              .AsNoTracking()
                                              .Where(e => e.EngageVariantProductId == variantId)
                                              .ProjectTo<EngageProductMasterVariantRelatedDto>(Mapper.ConfigurationProvider)
                                              .ToListAsync(cancellationToken);

                return dcProducts;
            }
        }
        throw new ArgumentException("Invalid GroupKeys");
    }
}