namespace Engage.Application.Services.SecurityPermissions.Queries;

public class SecurityPermissionOptionQuery : IRequest<List<SecurityPermissionOption>>
{ 

}

public record SecurityPermissionOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityPermissionOptionQuery, List<SecurityPermissionOption>>
{
    public async Task<List<SecurityPermissionOption>> Handle(SecurityPermissionOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SecurityPermissions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SecurityPermissionId)
                              .ProjectTo<SecurityPermissionOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}