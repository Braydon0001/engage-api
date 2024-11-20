namespace Engage.Application.Services.AnalysisPillarSubGroups.Queries;

public class AnalysisPillarSubGroupOptionQuery : IRequest<List<AnalysisPillarSubGroupOption>>
{
    public int? AnalysisPillarGroupId { get; set; }
}

public record AnalysisPillarSubGroupOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AnalysisPillarSubGroupOptionQuery, List<AnalysisPillarSubGroupOption>>
{
    public async Task<List<AnalysisPillarSubGroupOption>> Handle(AnalysisPillarSubGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.AnalysisPillarSubGroups.AsQueryable().AsNoTracking();

        if (query.AnalysisPillarGroupId.HasValue)
        {
            queryable = queryable.Where(e => e.AnalysisPillarGroupId == query.AnalysisPillarGroupId);
        }

        return await queryable.OrderBy(e => e.AnalysisPillarSubGroupId)
                              .ProjectTo<AnalysisPillarSubGroupOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}