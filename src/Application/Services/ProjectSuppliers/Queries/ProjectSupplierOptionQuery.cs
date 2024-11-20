namespace Engage.Application.Services.ProjectSuppliers.Queries;

public class ProjectSupplierOptionQuery : IRequest<List<ProjectSupplierOption>>
{ 

}

public record ProjectSupplierOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSupplierOptionQuery, List<ProjectSupplierOption>>
{
    public async Task<List<ProjectSupplierOption>> Handle(ProjectSupplierOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectSuppliers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectSupplierId)
                              .ProjectTo<ProjectSupplierOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}