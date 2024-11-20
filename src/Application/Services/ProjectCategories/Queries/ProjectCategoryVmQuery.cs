namespace Engage.Application.Services.ProjectCategories.Queries;

public record ProjectCategoryVmQuery(int Id) : IRequest<ProjectCategoryVm>;

public record ProjectCategoryVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCategoryVmQuery, ProjectCategoryVm>
{
    public async Task<ProjectCategoryVm> Handle(ProjectCategoryVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectCategories.AsQueryable().Include(e => e.ProjectCategorySuppliers).ThenInclude(e => e.Supplier).AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectCategoryId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectCategoryVm>(entity);
    }
}