namespace Engage.Application.Services.CreditorStatusHistories.Queries;

public record CreditorStatusHistoryVmQuery(int Id) : IRequest<CreditorStatusHistoryVm>;

public record CreditorStatusHistoryVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorStatusHistoryVmQuery, CreditorStatusHistoryVm>
{
    public async Task<CreditorStatusHistoryVm> Handle(CreditorStatusHistoryVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorStatusHistories.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Creditor)
                             .Include(e => e.CreditorStatus);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CreditorStatusHistoryId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CreditorStatusHistoryVm>(entity);
    }
}