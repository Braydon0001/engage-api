namespace Engage.Application.Services.CategorySubGroups.Queries;

public record CategorySubGroupVmQuery(int Id) : IRequest<CategorySubGroupVm>;

public record CategorySubGroupVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategorySubGroupVmQuery, CategorySubGroupVm>
{
    public async Task<CategorySubGroupVm> Handle(CategorySubGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategorySubGroups.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CategorySubGroupId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CategorySubGroupVm>(entity);
    }
}