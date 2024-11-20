// auto-generated
namespace Engage.Application.Services.InventoryTransactionStatuses.Queries;

public class InventoryTransactionStatusVmQuery : IRequest<InventoryTransactionStatusVm>
{
    public int Id { get; set; }
}

public class InventoryTransactionStatusVmHandler : VmQueryHandler, IRequestHandler<InventoryTransactionStatusVmQuery, InventoryTransactionStatusVm>
{
    public InventoryTransactionStatusVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryTransactionStatusVm> Handle(InventoryTransactionStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryTransactionStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.InventoryTransactionStatusId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<InventoryTransactionStatusVm>(entity);
    }
}