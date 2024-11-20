namespace Engage.Application.Services.CategoryStoreGroups.Queries;

public record CategoryStoreGroupVmQuery(int Id) : IRequest<CategoryStoreGroupVm>;

public record CategoryStoreGroupVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryStoreGroupVmQuery, CategoryStoreGroupVm>
{
    public async Task<CategoryStoreGroupVm> Handle(CategoryStoreGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryStoreGroups.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.CategoryGroup)
                             .Include(e => e.Store);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CategoryStoreGroupId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CategoryStoreGroupVm>(entity);
    }
}