namespace Engage.Application.Services.ProjectExternalUsers.Queries;

public class ProjectExternalUserOptionQuery : IRequest<List<ProjectExternalUserOption>>
{ 

}

public record ProjectExternalUserOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectExternalUserOptionQuery, List<ProjectExternalUserOption>>
{
    public async Task<List<ProjectExternalUserOption>> Handle(ProjectExternalUserOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectExternalUsers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectExternalUserId)
                              .ProjectTo<ProjectExternalUserOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}