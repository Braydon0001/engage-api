namespace Engage.Application.Services.SparAnalysisGroups.Queries;

public class SparAnalysisGroupListQuery : IRequest<List<SparAnalysisGroupDto>>
{

}

public record SparAnalysisGroupListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparAnalysisGroupListQuery, List<SparAnalysisGroupDto>>
{
    public async Task<List<SparAnalysisGroupDto>> Handle(SparAnalysisGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparAnalysisGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparAnalysisGroupId)
                              .ProjectTo<SparAnalysisGroupDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}