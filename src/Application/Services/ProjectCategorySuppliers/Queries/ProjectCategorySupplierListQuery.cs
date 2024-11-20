namespace Engage.Application.Services.ProjectCategorySuppliers.Queries;

public class ProjectCategorySupplierListQuery : IRequest<List<ProjectCategorySupplierDto>>
{

}

public record ProjectCategorySupplierListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCategorySupplierListQuery, List<ProjectCategorySupplierDto>>
{
    public async Task<List<ProjectCategorySupplierDto>> Handle(ProjectCategorySupplierListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectCategorySuppliers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectCategorySupplierId)
                              .ProjectTo<ProjectCategorySupplierDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}