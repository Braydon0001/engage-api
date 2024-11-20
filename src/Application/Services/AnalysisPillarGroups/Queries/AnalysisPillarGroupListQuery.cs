namespace Engage.Application.Services.AnalysisPillarGroups.Queries;

public class AnalysisPillarGroupListQuery : IRequest<List<AnalysisPillarGroupDto>>
{

}

public record AnalysisPillarGroupListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AnalysisPillarGroupListQuery, List<AnalysisPillarGroupDto>>
{
    public async Task<List<AnalysisPillarGroupDto>> Handle(AnalysisPillarGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.AnalysisPillarGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.AnalysisPillarGroupId)
                              .ProjectTo<AnalysisPillarGroupDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}