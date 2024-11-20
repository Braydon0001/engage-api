// auto-generated
namespace Engage.Application.Services.InventoryWarehouses.Queries;

public class InventoryWarehouseOptionListQuery : IRequest<List<InventoryWarehouseOption>>
{ 

}

public class InventoryWarehouseOptionListHandler : ListQueryHandler, IRequestHandler<InventoryWarehouseOptionListQuery, List<InventoryWarehouseOption>>
{
    public InventoryWarehouseOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryWarehouseOption>> Handle(InventoryWarehouseOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryWarehouses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryWarehouseOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}