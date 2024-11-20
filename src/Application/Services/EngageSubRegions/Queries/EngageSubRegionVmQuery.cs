namespace Engage.Application.Services.EngageSubRegions.Queries;

public record EngageSubRegionVmQuery(int Id) : IRequest<EngageSubRegionVm>;

public record EngageSubRegionVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageSubRegionVmQuery, EngageSubRegionVm>
{
    public async Task<EngageSubRegionVm> Handle(EngageSubRegionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EngageSubRegions.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.EngageRegion);

        var entity = await queryable.SingleOrDefaultAsync(e => e.EngageSubRegionId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<EngageSubRegionVm>(entity);
    }
}