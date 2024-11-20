// auto-generated
namespace Engage.Application.Services.ProductTransactionTypes.Queries;

public class ProductTransactionTypeVmQuery : IRequest<ProductTransactionTypeVm>
{
    public int Id { get; set; }
}

public class ProductTransactionTypeVmHandler : VmQueryHandler, IRequestHandler<ProductTransactionTypeVmQuery, ProductTransactionTypeVm>
{
    public ProductTransactionTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductTransactionTypeVm> Handle(ProductTransactionTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductTransactionTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductTransactionTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductTransactionTypeVm>(entity);
    }
}