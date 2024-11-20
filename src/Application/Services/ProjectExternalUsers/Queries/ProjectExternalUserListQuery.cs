namespace Engage.Application.Services.ProjectExternalUsers.Queries;

public class ProjectExternalUserListQuery : IRequest<List<ProjectExternalUserDto>>
{

}

public record ProjectExternalUserListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectExternalUserListQuery, List<ProjectExternalUserDto>>
{
    public async Task<List<ProjectExternalUserDto>> Handle(ProjectExternalUserListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectExternalUsers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectExternalUserId)
                              .ProjectTo<ProjectExternalUserDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}