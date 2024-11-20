namespace Engage.Application.Services.Inventories.Queries;

public record InventoryVmQuery(int Id) : IRequest<InventoryVm>;

public record InventoryVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryVmQuery, InventoryVm>
{
    public async Task<InventoryVm> Handle(InventoryVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Inventories.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.InventoryGroup)
                             .Include(e => e.InventoryStatus)
                             .Include(e => e.InventoryUnitType);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.InventoryId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<InventoryVm>(entity);
    }
}