// auto-generated
namespace Engage.Application.Services.InventoryTransactionTypes.Queries;

public class InventoryTransactionTypeVmQuery : IRequest<InventoryTransactionTypeVm>
{
    public int Id { get; set; }
}

public class InventoryTransactionTypeVmHandler : VmQueryHandler, IRequestHandler<InventoryTransactionTypeVmQuery, InventoryTransactionTypeVm>
{
    public InventoryTransactionTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryTransactionTypeVm> Handle(InventoryTransactionTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryTransactionTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.InventoryTransactionTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<InventoryTransactionTypeVm>(entity);
    }
}