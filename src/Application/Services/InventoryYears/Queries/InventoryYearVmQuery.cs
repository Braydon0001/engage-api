namespace Engage.Application.Services.InventoryYears.Queries;

public record InventoryYearVmQuery(int Id) : IRequest<InventoryYearVm>;

public record InventoryYearVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryYearVmQuery, InventoryYearVm>
{
    public async Task<InventoryYearVm> Handle(InventoryYearVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.InventoryYears.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.InventoryYearId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<InventoryYearVm>(entity);
    }
}