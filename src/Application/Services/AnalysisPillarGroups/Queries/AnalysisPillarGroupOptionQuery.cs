namespace Engage.Application.Services.AnalysisPillarGroups.Queries;

public class AnalysisPillarGroupOptionQuery : IRequest<List<AnalysisPillarGroupOption>>
{ 

}

public record AnalysisPillarGroupOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AnalysisPillarGroupOptionQuery, List<AnalysisPillarGroupOption>>
{
    public async Task<List<AnalysisPillarGroupOption>> Handle(AnalysisPillarGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.AnalysisPillarGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.AnalysisPillarGroupId)
                              .ProjectTo<AnalysisPillarGroupOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}