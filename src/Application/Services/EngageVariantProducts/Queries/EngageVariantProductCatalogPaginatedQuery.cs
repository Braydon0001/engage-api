using Engage.Application.Services.EngageVariantProducts.Models;

namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductRelatedCatalogPaginatedQuery : PaginatedQuery, IRequest<List<EngageVariantProductCatalogDto>>
{
    public string Search { get; set; }
    public int Id { get; set; }
}

public record EngageVariantProductRelatedCatalogPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageVariantProductRelatedCatalogPaginatedQuery, List<EngageVariantProductCatalogDto>>
{
    public async Task<List<EngageVariantProductCatalogDto>> Handle(EngageVariantProductRelatedCatalogPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = EngageVariantProductPaginationProps.Create();

        var masterProductId = await Context.EngageVariantProducts.Where(e => e.EngageVariantProductId == query.Id)
                                                                 .Select(e => e.EngageMasterProductId)
                                                                 .SingleAsync(cancellationToken);

        var queryable = Context.EngageVariantProducts.Where(e => e.EngageMasterProductId == masterProductId).AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.Disabled == false);
        queryable = queryable.Where(e => e.EngageVariantProductId != query.Id);

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


