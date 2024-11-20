namespace Engage.Application.Services.SparProductStatuses.Queries;

public record SparProductStatusVmQuery(int Id) : IRequest<SparProductStatusVm>;

public record SparProductStatusVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparProductStatusVmQuery, SparProductStatusVm>
{
    public async Task<SparProductStatusVm> Handle(SparProductStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparProductStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SparProductStatusId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SparProductStatusVm>(entity);
    }
}