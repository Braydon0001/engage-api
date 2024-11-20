namespace Engage.Application.Services.CategoryTargets.Queries;

public record CategoryTargetVmQuery(int Id) : IRequest<CategoryTargetVm>;

public record CategoryTargetVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetVmQuery, CategoryTargetVm>
{
    public async Task<CategoryTargetVm> Handle(CategoryTargetVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryTargets.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Supplier);

        var entity = await queryable.SingleOrDefaultAsync(e => e.CategoryTargetId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CategoryTargetVm>(entity);
    }
}