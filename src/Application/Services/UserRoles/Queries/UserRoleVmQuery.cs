namespace Engage.Application.Services.UserRoles.Queries;

public record UserRoleVmQuery(int Id) : IRequest<UserRoleVm>;

public record UserRoleVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserRoleVmQuery, UserRoleVm>
{
    public async Task<UserRoleVm> Handle(UserRoleVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserRoles.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.UserRoleId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<UserRoleVm>(entity);
    }
}