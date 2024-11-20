namespace Engage.Application.Services.Roles.Queries;

public class RoleOptionQuery : IRequest<List<RoleOption>>
{ 

}

public record RoleOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<RoleOptionQuery, List<RoleOption>>
{
    public async Task<List<RoleOption>> Handle(RoleOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Roles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.RoleId)
                              .ProjectTo<RoleOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}