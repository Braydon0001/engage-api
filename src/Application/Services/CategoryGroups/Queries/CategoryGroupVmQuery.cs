namespace Engage.Application.Services.CategoryGroups.Queries;

public record CategoryGroupVmQuery(int Id) : IRequest<CategoryGroupVm>;

public record CategoryGroupVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryGroupVmQuery, CategoryGroupVm>
{
    public async Task<CategoryGroupVm> Handle(CategoryGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryGroups.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CategoryGroupId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CategoryGroupVm>(entity);
    }
}