namespace Engage.Application.Services.InventoryPeriods.Queries;

public record InventoryPeriodVmQuery(int Id) : IRequest<InventoryPeriodVm>;

public record InventoryPeriodVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryPeriodVmQuery, InventoryPeriodVm>
{
    public async Task<InventoryPeriodVm> Handle(InventoryPeriodVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.InventoryPeriods.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.InventoryYear);

        var entity = await queryable.SingleOrDefaultAsync(e => e.InventoryPeriodId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<InventoryPeriodVm>(entity);
    }
}