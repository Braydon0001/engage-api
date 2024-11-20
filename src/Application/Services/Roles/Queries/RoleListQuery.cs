namespace Engage.Application.Services.Roles.Queries;

public class RoleListQuery : IRequest<List<RoleDto>>
{

}

public record RoleListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<RoleListQuery, List<RoleDto>>
{
    public async Task<List<RoleDto>> Handle(RoleListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Roles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.RoleId)
                              .ProjectTo<RoleDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}