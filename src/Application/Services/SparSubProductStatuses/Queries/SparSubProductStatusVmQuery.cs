namespace Engage.Application.Services.SparSubProductStatuses.Queries;

public record SparSubProductStatusVmQuery(int Id) : IRequest<SparSubProductStatusVm>;

public record SparSubProductStatusVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSubProductStatusVmQuery, SparSubProductStatusVm>
{
    public async Task<SparSubProductStatusVm> Handle(SparSubProductStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparSubProductStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SparSubProductStatusId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SparSubProductStatusVm>(entity);
    }
}