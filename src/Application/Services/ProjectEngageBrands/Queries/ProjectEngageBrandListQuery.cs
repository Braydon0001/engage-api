namespace Engage.Application.Services.ProjectEngageBrands.Queries;

public class ProjectEngageBrandListQuery : IRequest<List<ProjectEngageBrandDto>>
{

}

public record ProjectEngageBrandListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectEngageBrandListQuery, List<ProjectEngageBrandDto>>
{
    public async Task<List<ProjectEngageBrandDto>> Handle(ProjectEngageBrandListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectEngageBrands.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectEngageBrandId)
                              .ProjectTo<ProjectEngageBrandDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}