// auto-generated
namespace Engage.Application.Services.ProductPeriods.Queries;

public class ProductPeriodListQuery : IRequest<List<ProductPeriodDto>>
{
    public int? ProductYearId { get; set; }
}

public class ProductPeriodListHandler : ListQueryHandler, IRequestHandler<ProductPeriodListQuery, List<ProductPeriodDto>>
{
    public ProductPeriodListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductPeriodDto>> Handle(ProductPeriodListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductPeriods.AsQueryable().AsNoTracking();

        if (query.ProductYearId.HasValue)
        {
            queryable = queryable.Where(e => e.ProductYearId == query.ProductYearId);
        }

        return await queryable.OrderBy(e => e.ProductYearId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<ProductPeriodDto>(_mapper.ConfigurationProvider)
                              .OrderBy(e => e.Id)
                              .ToListAsync(cancellationToken);
    }
}