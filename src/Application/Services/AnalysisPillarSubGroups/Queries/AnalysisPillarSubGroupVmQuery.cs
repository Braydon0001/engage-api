namespace Engage.Application.Services.AnalysisPillarSubGroups.Queries;

public record AnalysisPillarSubGroupVmQuery(int Id) : IRequest<AnalysisPillarSubGroupVm>;

public record AnalysisPillarSubGroupVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AnalysisPillarSubGroupVmQuery, AnalysisPillarSubGroupVm>
{
    public async Task<AnalysisPillarSubGroupVm> Handle(AnalysisPillarSubGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.AnalysisPillarSubGroups.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.AnalysisPillarGroup);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.AnalysisPillarSubGroupId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<AnalysisPillarSubGroupVm>(entity);
    }
}