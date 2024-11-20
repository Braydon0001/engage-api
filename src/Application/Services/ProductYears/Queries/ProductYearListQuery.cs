// auto-generated
namespace Engage.Application.Services.ProductYears.Queries;

public class ProductYearListQuery : IRequest<List<ProductYearDto>>
{

}

public class ProductYearListHandler : ListQueryHandler, IRequestHandler<ProductYearListQuery, List<ProductYearDto>>
{
    public ProductYearListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductYearDto>> Handle(ProductYearListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductYears.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductYearDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}