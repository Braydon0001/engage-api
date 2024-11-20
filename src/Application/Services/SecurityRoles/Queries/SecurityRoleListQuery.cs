namespace Engage.Application.Services.SecurityRoles.Queries;

public class SecurityRoleListQuery : IRequest<List<SecurityRoleDto>>
{

}

public record SecurityRoleListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityRoleListQuery, List<SecurityRoleDto>>
{
    public async Task<List<SecurityRoleDto>> Handle(SecurityRoleListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SecurityRoles.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SecurityPermissionRoles)
                             .ThenInclude(e => e.SecurityPermission);

        return await queryable.OrderBy(e => e.SecurityRoleId)
                              .ProjectTo<SecurityRoleDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}