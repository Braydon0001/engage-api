// auto-generated
namespace Engage.Application.Services.InventoryUnitTypes.Queries;

public class InventoryUnitTypeVmQuery : IRequest<InventoryUnitTypeVm>
{
    public int Id { get; set; }
}

public class InventoryUnitTypeVmHandler : VmQueryHandler, IRequestHandler<InventoryUnitTypeVmQuery, InventoryUnitTypeVm>
{
    public InventoryUnitTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryUnitTypeVm> Handle(InventoryUnitTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryUnitTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.InventoryUnitTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<InventoryUnitTypeVm>(entity);
    }
}