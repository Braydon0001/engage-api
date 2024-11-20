namespace Engage.Application.Services.UserOrganizationRoles.Queries;

public class UserOrganizationRoleOptionQuery : IRequest<List<UserOrganizationRoleOption>>
{ 

}

public record UserOrganizationRoleOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserOrganizationRoleOptionQuery, List<UserOrganizationRoleOption>>
{
    public async Task<List<UserOrganizationRoleOption>> Handle(UserOrganizationRoleOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserOrganizationRoles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.UserOrganizationRoleId)
                              .ProjectTo<UserOrganizationRoleOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}