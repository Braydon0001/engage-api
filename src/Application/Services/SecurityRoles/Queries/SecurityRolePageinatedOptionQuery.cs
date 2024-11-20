
namespace Engage.Application.Services.SecurityRoles.Queries;

public class SecurityRolePageinatedOptionQuery : IRequest<List<SecurityRoleOption>>
{
    public string Search { get; set; }
}
public record SecurityRolePageinatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityRolePageinatedOptionQuery, List<SecurityRoleOption>>
{
    public async Task<List<SecurityRoleOption>> Handle(SecurityRolePageinatedOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SecurityRoles.AsNoTracking()
                                            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{query.Search}%")
                                        || EF.Functions.Like(e.Key, $"%{query.Search}"));
        }

        return await queryable.Where(e => e.Disabled == false)
                              .OrderBy(e => e.Name)
                              .ProjectTo<SecurityRoleOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
