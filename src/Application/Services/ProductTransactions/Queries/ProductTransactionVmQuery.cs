// auto-generated
namespace Engage.Application.Services.ProductTransactions.Queries;

public class ProductTransactionVmQuery : IRequest<ProductTransactionVm>
{
    public int Id { get; set; }
}

public class ProductTransactionVmHandler : VmQueryHandler, IRequestHandler<ProductTransactionVmQuery, ProductTransactionVm>
{
    public ProductTransactionVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductTransactionVm> Handle(ProductTransactionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductTransactions.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Product)
                             .Include(e => e.ProductTransactionType)
                             .Include(e => e.ProductTransactionStatus)
                             .Include(e => e.ProductPeriod)
                             .Include(e => e.Employee)
                             .Include(e => e.ProductWarehouse);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductTransactionId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductTransactionVm>(entity);
    }
}