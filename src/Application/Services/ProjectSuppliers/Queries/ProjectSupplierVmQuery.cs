namespace Engage.Application.Services.ProjectSuppliers.Queries;

public record ProjectSupplierVmQuery(int Id) : IRequest<ProjectSupplierVm>;

public record ProjectSupplierVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSupplierVmQuery, ProjectSupplierVm>
{
    public async Task<ProjectSupplierVm> Handle(ProjectSupplierVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectSuppliers.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Project)
                             .Include(e => e.Supplier);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectSupplierId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectSupplierVm>(entity);
    }
}