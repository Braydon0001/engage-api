namespace Engage.Application.Services.SecurityRoles.Queries;

public class SecurityRoleOptionQuery : IRequest<List<SecurityRoleOption>>
{ 

}

public record SecurityRoleOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityRoleOptionQuery, List<SecurityRoleOption>>
{
    public async Task<List<SecurityRoleOption>> Handle(SecurityRoleOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SecurityRoles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SecurityRoleId)
                              .ProjectTo<SecurityRoleOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}