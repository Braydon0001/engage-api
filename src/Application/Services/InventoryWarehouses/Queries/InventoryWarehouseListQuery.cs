// auto-generated
namespace Engage.Application.Services.InventoryWarehouses.Queries;

public class InventoryWarehouseListQuery : IRequest<List<InventoryWarehouseDto>>
{

}

public class InventoryWarehouseListHandler : ListQueryHandler, IRequestHandler<InventoryWarehouseListQuery, List<InventoryWarehouseDto>>
{
    public InventoryWarehouseListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryWarehouseDto>> Handle(InventoryWarehouseListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryWarehouses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryWarehouseDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}