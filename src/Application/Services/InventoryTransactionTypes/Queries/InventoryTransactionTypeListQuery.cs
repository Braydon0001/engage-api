// auto-generated
namespace Engage.Application.Services.InventoryTransactionTypes.Queries;

public class InventoryTransactionTypeListQuery : IRequest<List<InventoryTransactionTypeDto>>
{

}

public class InventoryTransactionTypeListHandler : ListQueryHandler, IRequestHandler<InventoryTransactionTypeListQuery, List<InventoryTransactionTypeDto>>
{
    public InventoryTransactionTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryTransactionTypeDto>> Handle(InventoryTransactionTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryTransactionTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryTransactionTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}