namespace Engage.Application.Services.CategoryTargetStores.Queries;

public record CategoryTargetStoreVmQuery(int Id) : IRequest<CategoryTargetStoreVm>;

public record CategoryTargetStoreVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetStoreVmQuery, CategoryTargetStoreVm>
{
    public async Task<CategoryTargetStoreVm> Handle(CategoryTargetStoreVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryTargetStores.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.CategoryTarget)
                             .Include(e => e.Store);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CategoryTargetStoreId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CategoryTargetStoreVm>(entity);
    }
}