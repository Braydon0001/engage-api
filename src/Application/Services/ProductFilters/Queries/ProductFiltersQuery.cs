using Engage.Application.Services.ProductFilters.Models;

namespace Engage.Application.Services.ProductFilters.Queries;

public class ProductFiltersQuery : IRequest<ListResult<ProductFilterDto>>
{
    public List<int> ProductIds { get; set; }
    public List<string> Filters { get; set; }
}

public class ProductFiltersQueryHandler : BaseQueryHandler, IRequestHandler<ProductFiltersQuery, ListResult<ProductFilterDto>>
{
    public ProductFiltersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ProductFilterDto>> Handle(ProductFiltersQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductFilters.AsQueryable();

        if (request.ProductIds != null && request.ProductIds.Count > 0)
        {
            queryable = queryable.Where(e => request.ProductIds.Contains(e.EngageVariantProductId.Value));
        }

        if (request.Filters != null && request.Filters.Count > 0)
        {
            queryable = queryable.Where(e => request.Filters.Contains(e.Filter));
        }

        var entities = await _context.ProductFilters.OrderBy(e => e.Filter)
                                                    .ThenBy(e => e.EngageVariantProduct.Name)
                                                    .ProjectTo<ProductFilterDto>(_mapper.ConfigurationProvider)
                                                    .ToListAsync(cancellationToken);

        return new ListResult<ProductFilterDto>(entities);
    }
}
