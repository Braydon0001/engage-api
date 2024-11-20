namespace Engage.Application.Services.SecurityPermissions.Queries;

public class SecurityPermissionListQuery : IRequest<List<SecurityPermissionDto>>
{

}

public record SecurityPermissionListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityPermissionListQuery, List<SecurityPermissionDto>>
{
    public async Task<List<SecurityPermissionDto>> Handle(SecurityPermissionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SecurityPermissions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SecurityPermissionId)
                              .ProjectTo<SecurityPermissionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}