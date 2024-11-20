// auto-generated
namespace Engage.Application.Services.ProductPeriods.Queries;

public class ProductPeriodOptionListQuery : IRequest<List<ProductPeriodOption>>
{
    public int? ProductYearId { get; set; }
}

public class ProductPeriodOptionListHandler : ListQueryHandler, IRequestHandler<ProductPeriodOptionListQuery, List<ProductPeriodOption>>
{
    public ProductPeriodOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductPeriodOption>> Handle(ProductPeriodOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductPeriods.AsQueryable().AsNoTracking();

        if (query.ProductYearId.HasValue)
        {
            queryable = queryable.Where(e => e.ProductYearId == query.ProductYearId);
        }

        return await queryable.OrderBy(e => e.ProductYearId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<ProductPeriodOption>(_mapper.ConfigurationProvider)
                              .OrderBy(e => e.Id)
                              .ToListAsync(cancellationToken);
    }
}