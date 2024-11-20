namespace Engage.Application.Services.SparSystemStatuses.Queries;

public record SparSystemStatusVmQuery(int Id) : IRequest<SparSystemStatusVm>;

public record SparSystemStatusVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSystemStatusVmQuery, SparSystemStatusVm>
{
    public async Task<SparSystemStatusVm> Handle(SparSystemStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparSystemStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SparSystemStatusId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SparSystemStatusVm>(entity);
    }
}