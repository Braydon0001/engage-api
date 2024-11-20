namespace Engage.Application.Services.UserStores.Queries;

public record UserStoreVmQuery(int Id) : IRequest<UserStoreVm>;

public record UserStoreVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserStoreVmQuery, UserStoreVm>
{
    public async Task<UserStoreVm> Handle(UserStoreVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserStores.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.User)
                             .Include(e => e.Store);

        var entity = await queryable.SingleOrDefaultAsync(e => e.UserStoreId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<UserStoreVm>(entity);
    }
}