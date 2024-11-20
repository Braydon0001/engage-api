// auto-generated
namespace Engage.Application.Services.ProductYears.Queries;

public class ProductYearOptionListQuery : IRequest<List<ProductYearOption>>
{

}

public class ProductYearOptionListHandler : ListQueryHandler, IRequestHandler<ProductYearOptionListQuery, List<ProductYearOption>>
{
    public ProductYearOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductYearOption>> Handle(ProductYearOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductYears.AsQueryable().AsNoTracking();

        return await queryable.OrderByDescending(e => e.ProductYearId)
                              .ProjectTo<ProductYearOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}