// auto-generated
namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public class ProductWarehouseSummaryListQuery : IRequest<List<ProductWarehouseSummaryDto>>
{

}

public class ProductWarehouseSummaryListHandler : ListQueryHandler, IRequestHandler<ProductWarehouseSummaryListQuery, List<ProductWarehouseSummaryDto>>
{
    public ProductWarehouseSummaryListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductWarehouseSummaryDto>> Handle(ProductWarehouseSummaryListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductWarehouseSummaries.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductWarehouseSummaryId)
                              .ProjectTo<ProductWarehouseSummaryDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}