// auto-generated
namespace Engage.Application.Services.InventoryTransactions.Queries;

public class InventoryTransactionVmQuery : IRequest<InventoryTransactionVm>
{
    public int Id { get; set; }
}

public class InventoryTransactionVmHandler : VmQueryHandler, IRequestHandler<InventoryTransactionVmQuery, InventoryTransactionVm>
{
    public InventoryTransactionVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryTransactionVm> Handle(InventoryTransactionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryTransactions.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.InventoryTransactionType)
                             .Include(e => e.InventoryTransactionStatus)
                             .Include(e => e.Inventory)
                             .Include(e => e.InventoryWarehouse);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.InventoryTransactionId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<InventoryTransactionVm>(entity);
    }
}