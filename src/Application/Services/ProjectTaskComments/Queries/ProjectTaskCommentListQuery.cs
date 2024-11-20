namespace Engage.Application.Services.ProjectTaskComments.Queries;

public class ProjectTaskCommentListQuery : IRequest<List<ProjectTaskCommentDto>>
{

}

public record ProjectTaskCommentListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskCommentListQuery, List<ProjectTaskCommentDto>>
{
    public async Task<List<ProjectTaskCommentDto>> Handle(ProjectTaskCommentListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskComments.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTaskCommentId)
                              .ProjectTo<ProjectTaskCommentDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}