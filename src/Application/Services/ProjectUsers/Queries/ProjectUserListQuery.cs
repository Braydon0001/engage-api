namespace Engage.Application.Services.ProjectUsers.Queries;

public class ProjectUserListQuery : IRequest<List<ProjectUserDto>>
{

}

public record ProjectUserListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectUserListQuery, List<ProjectUserDto>>
{
    public async Task<List<ProjectUserDto>> Handle(ProjectUserListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectUsers.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.UserId)
                              .ProjectTo<ProjectUserDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}