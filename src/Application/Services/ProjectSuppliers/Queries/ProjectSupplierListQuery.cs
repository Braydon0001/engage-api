namespace Engage.Application.Services.ProjectSuppliers.Queries;

public class ProjectSupplierListQuery : IRequest<List<ProjectSupplierDto>>
{

}

public record ProjectSupplierListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSupplierListQuery, List<ProjectSupplierDto>>
{
    public async Task<List<ProjectSupplierDto>> Handle(ProjectSupplierListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectSuppliers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectSupplierId)
                              .ProjectTo<ProjectSupplierDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}