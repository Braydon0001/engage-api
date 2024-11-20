namespace Engage.Application.Services.UserOrganizationRoles.Queries;

public class UserOrganizationRoleListQuery : IRequest<List<UserOrganizationRoleDto>>
{

}

public record UserOrganizationRoleListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserOrganizationRoleListQuery, List<UserOrganizationRoleDto>>
{
    public async Task<List<UserOrganizationRoleDto>> Handle(UserOrganizationRoleListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserOrganizationRoles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.UserOrganizationRoleId)
                              .ProjectTo<UserOrganizationRoleDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}