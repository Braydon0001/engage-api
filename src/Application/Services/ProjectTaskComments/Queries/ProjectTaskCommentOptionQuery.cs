namespace Engage.Application.Services.ProjectTaskComments.Queries;

public class ProjectTaskCommentOptionQuery : IRequest<List<ProjectTaskCommentOption>>
{ 

}

public record ProjectTaskCommentOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskCommentOptionQuery, List<ProjectTaskCommentOption>>
{
    public async Task<List<ProjectTaskCommentOption>> Handle(ProjectTaskCommentOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskComments.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTaskCommentId)
                              .ProjectTo<ProjectTaskCommentOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}