namespace Engage.Application.Services.ProjectComments.Queries;

public class ProjectCommentOptionQuery : IRequest<List<ProjectCommentOption>>
{ 

}

public record ProjectCommentOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCommentOptionQuery, List<ProjectCommentOption>>
{
    public async Task<List<ProjectCommentOption>> Handle(ProjectCommentOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectComments.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectCommentId)
                              .ProjectTo<ProjectCommentOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}