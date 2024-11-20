namespace Engage.Application.Services.SparProducts.Queries;

public class SparProductPaginatedQuery : PaginatedQuery, IRequest<List<SparProductDto>>
{
    public string Search { get; set; }
}

public record SparProductPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparProductPaginatedQuery, List<SparProductDto>>
{
    public async Task<List<SparProductDto>> Handle(SparProductPaginatedQuery query, CancellationToken cancellationToken)
    {
        if (query.GroupKeys.Count == 0 && !query.Search.IsEmpty()) //no query keys and search
        {
            var sparSubProducts = await Context.SparSubProducts.AsNoTracking()
                                                               .Where(e => EF.Functions.Like(e.Barcode, $"%{query.Search}%")
                                                                        || EF.Functions.Like(e.DCCode, $"%{query.Search}%")
                                                                        || EF.Functions.Like(e.Name, $"%{query.Search}%"))
                                                               .OrderBy(e => e.SparProductId)
                                                               .SkipQuery(query)
                                                               .TakeQuery(query)
                                                               .Select(e => e.SparProductId)
                                                               .ToListAsync(cancellationToken);

            var sparProducts = await Context.SparProducts.AsNoTracking()
                                                         .Where(e => (EF.Functions.Like(e.Barcode, $"%{query.Search}%")
                                                                   || EF.Functions.Like(e.ItemCode, $"%{query.Search}%")
                                                                   || EF.Functions.Like(e.Name, $"%{query.Search}%"))
                                                                   || sparSubProducts.Contains(e.SparProductId))
                                                         .OrderBy(e => e.Name)
                                                         .SkipQuery(query)
                                                         .TakeQuery(query)
                                                         .ProjectTo<SparProductDto>(Mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);

            sparProducts.ForEach(product =>
            {
                product.IsParent = true;
            });

            return sparProducts;

        }
        if (query.GroupKeys.Count == 0) //no query keys or search parameters
        {
            var props = SparProductPaginationProps.Create();

            var queryable = Context.SparProducts.AsQueryable().AsNoTracking();

            var entities = await queryable.Filter(query, props)
                                  .Sort(query, props)
                                  .SkipQuery(query)
                                  .TakeQuery(query)
                                  .OrderByDescending(e => e.SparProductId)
                                  .ProjectTo<SparProductDto>(Mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);

            entities.ForEach(product =>
            {
                product.IsParent = true;
            });

            return entities;
        }
        if (query.GroupKeys.Count == 1)
        {
            var SparProductId = int.Parse(query.GroupKeys[0]);

            var products = await Context.SparSubProducts.AsNoTracking()
                                                        .IgnoreQueryFilters()
                                                        .Where(e => e.SparProductId == SparProductId)
                                                        .ProjectTo<SparProductDto>(Mapper.ConfigurationProvider)
                                                        .ToListAsync(cancellationToken);
            products.ForEach(Product =>
            {
                Product.IsParent = false;
            });

            return products;
        }
        throw new ArgumentException("Invalid GroupKeys");
    }
}


