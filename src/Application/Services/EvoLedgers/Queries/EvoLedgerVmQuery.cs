namespace Engage.Application.Services.EvoLedgers.Queries;

public record EvoLedgerVmQuery(int Id) : IRequest<EvoLedgerVm>;

public record EvoLedgerVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EvoLedgerVmQuery, EvoLedgerVm>
{
    public async Task<EvoLedgerVm> Handle(EvoLedgerVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EvoLedgers.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.AnalysisPillarSubGroup)
                             .ThenInclude(e => e.AnalysisPillarGroup);

        var entity = await queryable.SingleOrDefaultAsync(e => e.EvoLedgerId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<EvoLedgerVm>(entity);
    }
}