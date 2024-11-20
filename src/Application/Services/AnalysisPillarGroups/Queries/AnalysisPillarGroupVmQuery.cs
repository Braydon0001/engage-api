namespace Engage.Application.Services.AnalysisPillarGroups.Queries;

public record AnalysisPillarGroupVmQuery(int Id) : IRequest<AnalysisPillarGroupVm>;

public record AnalysisPillarGroupVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AnalysisPillarGroupVmQuery, AnalysisPillarGroupVm>
{
    public async Task<AnalysisPillarGroupVm> Handle(AnalysisPillarGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.AnalysisPillarGroups.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.AnalysisPillarGroupId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<AnalysisPillarGroupVm>(entity);
    }
}