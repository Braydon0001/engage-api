using Engage.Application.Services.EngageMasterProducts.Models;

namespace Engage.Application.Services.EngageMasterProducts.Queries;

public class EngageMasterProductVariantPaginatedQuery : PaginatedQuery, IRequest<List<EngageProductMasterVariantDto>>
{
    public string Search { get; set; }
}
public record EngageMasterProductVariantPaginatedhandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageMasterProductVariantPaginatedQuery, List<EngageProductMasterVariantDto>>
{
    public async Task<List<EngageProductMasterVariantDto>> Handle(EngageMasterProductVariantPaginatedQuery query, CancellationToken cancellationToken)
    {
        if (query.GroupKeys.Count == 0 && !query.Search.IsEmpty())
        {
            var dcProductIds = await Context.DCProducts.AsNoTracking()
                                             .Where(e => EF.Functions.Like(e.Code, $"%{query.Search}%")
                                                        || EF.Functions.Like(e.Name, $"%{query.Search}%"))
                                             .OrderBy(e => e.EngageVariantProductId)
                                             .SkipQuery(query)
                                             .TakeQuery(query)
                                             .Select(e => e.EngageVariantProductId)
                                             .ToListAsync(cancellationToken);
            dcProductIds = dcProductIds.Distinct().ToList();

            var variantProducts = await Context.EngageVariantProducts.AsNoTracking()
                                               .Where(e => EF.Functions.Like(e.Code, $"%{query.Search}%")
                                                        || EF.Functions.Like(e.Name, $"%{query.Search}%")
                                                        || dcProductIds.Contains(e.EngageVariantProductId))
                                               .OrderBy(e => e.EngageVariantProductId)
                                               .SkipQuery(query)
                                               .TakeQuery(query)
                                               .ProjectTo<EngageProductMasterVariantDto>(Mapper.ConfigurationProvider)
                                               .ToListAsync(cancellationToken);

            return variantProducts;
        }
        if (query.GroupKeys.Count == 0)
        {
            var queryable = Context.EngageVariantProducts.AsQueryable().AsNoTracking();

            if (query.SortModel.IsNullOrEmpty())
            {
                queryable = queryable.OrderByDescending(e => e.EngageVariantProductId);
            }

            var variantProducts = await queryable.SkipQuery(query)
                                           .TakeQuery(query)
                                           .ProjectTo<EngageProductMasterVariantDto>(Mapper.ConfigurationProvider)
                                           .ToListAsync(cancellationToken);

            return variantProducts;
        }
        if (query.GroupKeys.Count == 1)
        {
            var variantproductId = int.Parse(query.GroupKeys[0]);

            var dcProducts = await Context.DCProducts.AsNoTracking()
                                          .IgnoreQueryFilters()
                                          .Where(e => e.EngageVariantProductId == variantproductId)
                                          .ProjectTo<EngageProductMasterVariantDto>(Mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return dcProducts;
        }
        throw new ArgumentException("Invalid GroupKeys");
    }
}
