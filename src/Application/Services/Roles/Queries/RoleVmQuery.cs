namespace Engage.Application.Services.Roles.Queries;

public record RoleVmQuery(int Id) : IRequest<RoleVm>;

public record RoleVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<RoleVmQuery, RoleVm>
{
    public async Task<RoleVm> Handle(RoleVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Roles.AsQueryable().AsNoTracking();

        var entity = await queryable.Include(e => e.RoleUserGroups)
                                        .ThenInclude(e => e.UserGroup)
                                    .SingleOrDefaultAsync(e => e.RoleId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<RoleVm>(entity);
    }
}