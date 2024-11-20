namespace Engage.Application.Services.Roles.Queries;

public record RolePermissionVmQuery(int Id) : IRequest<RolePermissionVm>;

public record RolePermissionVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<RolePermissionVmQuery, RolePermissionVm>
{
    public async Task<RolePermissionVm> Handle(RolePermissionVmQuery query, CancellationToken cancellationToken)
    {
        var role = await Context.Roles.Include(e => e.RoleUserGroups)
                                                .ThenInclude(e => e.UserGroup)
                                           .SingleOrDefaultAsync(e => e.RoleId == query.Id, cancellationToken);

        var allGroups = await Context.UserGroups.IgnoreQueryFilters()
                                                .Where(e => !e.Deleted && !e.Deleted && e.Name != "Everyone")
                                                .ProjectTo<RolePermission>(Mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);

        var entity = new RolePermissionVm()
        {
            Id = role.RoleId,
            Name = role.Name,
            Description = role.Description,
            Permissions = allGroups.Select(e =>
            {
                e.InRole = role.RoleUserGroups.Any(rug => rug.UserGroupId == e.Id);
                return e;
            }).ToList()
        };

        return entity;
    }
}