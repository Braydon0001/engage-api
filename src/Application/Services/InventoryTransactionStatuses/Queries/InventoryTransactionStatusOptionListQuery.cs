// auto-generated
namespace Engage.Application.Services.InventoryTransactionStatuses.Queries;

public class InventoryTransactionStatusOptionListQuery : IRequest<List<InventoryTransactionStatusOption>>
{ 

}

public class InventoryTransactionStatusOptionListHandler : ListQueryHandler, IRequestHandler<InventoryTransactionStatusOptionListQuery, List<InventoryTransactionStatusOption>>
{
    public InventoryTransactionStatusOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryTransactionStatusOption>> Handle(InventoryTransactionStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryTransactionStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryTransactionStatusOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}