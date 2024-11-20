namespace Engage.Application.Services.SparAnalysisGroups.Queries;

public class SparAnalysisGroupOptionQuery : IRequest<List<SparAnalysisGroupOption>>
{ 

}

public record SparAnalysisGroupOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparAnalysisGroupOptionQuery, List<SparAnalysisGroupOption>>
{
    public async Task<List<SparAnalysisGroupOption>> Handle(SparAnalysisGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparAnalysisGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparAnalysisGroupId)
                              .ProjectTo<SparAnalysisGroupOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}