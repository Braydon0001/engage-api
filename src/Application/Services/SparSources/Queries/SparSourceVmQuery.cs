namespace Engage.Application.Services.SparSources.Queries;

public record SparSourceVmQuery(int Id) : IRequest<SparSourceVm>;

public record SparSourceVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSourceVmQuery, SparSourceVm>
{
    public async Task<SparSourceVm> Handle(SparSourceVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparSources.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SparSourceId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SparSourceVm>(entity);
    }
}