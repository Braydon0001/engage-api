namespace Engage.Application.Services.SupplierSubRegions.Queries;

public record SupplierSubRegionVmQuery(int Id) : IRequest<SupplierSubRegionVm>;

public record SupplierSubRegionVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SupplierSubRegionVmQuery, SupplierSubRegionVm>
{
    public async Task<SupplierSubRegionVm> Handle(SupplierSubRegionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SupplierSubRegions.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SupplierRegion);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierSubRegionId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SupplierSubRegionVm>(entity);
    }
}