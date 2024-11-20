// auto-generated
namespace Engage.Application.Services.InventoryWarehouses.Queries;

public class InventoryWarehouseVmQuery : IRequest<InventoryWarehouseVm>
{
    public int Id { get; set; }
}

public class InventoryWarehouseVmHandler : VmQueryHandler, IRequestHandler<InventoryWarehouseVmQuery, InventoryWarehouseVm>
{
    public InventoryWarehouseVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryWarehouseVm> Handle(InventoryWarehouseVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryWarehouses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.InventoryWarehouseId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<InventoryWarehouseVm>(entity);
    }
}