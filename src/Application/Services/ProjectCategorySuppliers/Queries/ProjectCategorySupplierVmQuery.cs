namespace Engage.Application.Services.ProjectCategorySuppliers.Queries;

public record ProjectCategorySupplierVmQuery(int Id) : IRequest<ProjectCategorySupplierVm>;

public record ProjectCategorySupplierVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCategorySupplierVmQuery, ProjectCategorySupplierVm>
{
    public async Task<ProjectCategorySupplierVm> Handle(ProjectCategorySupplierVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectCategorySuppliers.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProjectCategory)
                             .Include(e => e.Supplier);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectCategorySupplierId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectCategorySupplierVm>(entity);
    }
}