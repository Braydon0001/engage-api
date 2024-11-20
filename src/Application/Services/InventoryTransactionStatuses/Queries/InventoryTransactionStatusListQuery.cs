// auto-generated
namespace Engage.Application.Services.InventoryTransactionStatuses.Queries;

public class InventoryTransactionStatusListQuery : IRequest<List<InventoryTransactionStatusDto>>
{

}

public class InventoryTransactionStatusListHandler : ListQueryHandler, IRequestHandler<InventoryTransactionStatusListQuery, List<InventoryTransactionStatusDto>>
{
    public InventoryTransactionStatusListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryTransactionStatusDto>> Handle(InventoryTransactionStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryTransactionStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryTransactionStatusDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}