namespace Engage.Application.Services.SparUnitTypes.Queries;

public record SparUnitTypeVmQuery(int Id) : IRequest<SparUnitTypeVm>;

public record SparUnitTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparUnitTypeVmQuery, SparUnitTypeVm>
{
    public async Task<SparUnitTypeVm> Handle(SparUnitTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparUnitTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SparUnitTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SparUnitTypeVm>(entity);
    }
}