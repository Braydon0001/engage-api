// auto-generated
namespace Engage.Application.Services.InventoryTransactionTypes.Queries;

public class InventoryTransactionTypeOptionListQuery : IRequest<List<InventoryTransactionTypeOption>>
{ 

}

public class InventoryTransactionTypeOptionListHandler : ListQueryHandler, IRequestHandler<InventoryTransactionTypeOptionListQuery, List<InventoryTransactionTypeOption>>
{
    public InventoryTransactionTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryTransactionTypeOption>> Handle(InventoryTransactionTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryTransactionTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryTransactionTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}