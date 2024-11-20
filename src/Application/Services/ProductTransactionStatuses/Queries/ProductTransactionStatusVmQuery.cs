// auto-generated
namespace Engage.Application.Services.ProductTransactionStatuses.Queries;

public class ProductTransactionStatusVmQuery : IRequest<ProductTransactionStatusVm>
{
    public int Id { get; set; }
}

public class ProductTransactionStatusVmHandler : VmQueryHandler, IRequestHandler<ProductTransactionStatusVmQuery, ProductTransactionStatusVm>
{
    public ProductTransactionStatusVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductTransactionStatusVm> Handle(ProductTransactionStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductTransactionStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductTransactionStatusId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductTransactionStatusVm>(entity);
    }
}