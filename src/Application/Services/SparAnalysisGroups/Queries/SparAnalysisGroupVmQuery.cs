namespace Engage.Application.Services.SparAnalysisGroups.Queries;

public record SparAnalysisGroupVmQuery(int Id) : IRequest<SparAnalysisGroupVm>;

public record SparAnalysisGroupVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparAnalysisGroupVmQuery, SparAnalysisGroupVm>
{
    public async Task<SparAnalysisGroupVm> Handle(SparAnalysisGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparAnalysisGroups.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SparAnalysisGroupId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SparAnalysisGroupVm>(entity);
    }
}