namespace Engage.Application.Services.Creditors.Queries;

public record CreditorVmQuery(int Id) : IRequest<CreditorVm>;

public record CreditorVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorVmQuery, CreditorVm>
{
    public async Task<CreditorVm> Handle(CreditorVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Creditors.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.CreditorStatus);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CreditorId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CreditorVm>(entity);
    }
}