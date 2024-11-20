// auto-generated
namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public class ProductWarehouseSummaryVmQuery : IRequest<ProductWarehouseSummaryVm>
{
    public int Id { get; set; }
}

public class ProductWarehouseSummaryVmHandler : VmQueryHandler, IRequestHandler<ProductWarehouseSummaryVmQuery, ProductWarehouseSummaryVm>
{
    public ProductWarehouseSummaryVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductWarehouseSummaryVm> Handle(ProductWarehouseSummaryVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductWarehouseSummaries.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Product)
                             .Include(e => e.ProductWarehouse)
                             .Include(e => e.ProductPeriod);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductWarehouseSummaryId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductWarehouseSummaryVm>(entity);
    }
}