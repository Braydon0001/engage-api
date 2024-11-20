using Engage.Application.Services.Suppliers.Queries;

namespace Engage.Application.Services.ProjectCategorySuppliers.Queries;

public class ProjectCategorySupplierOptionQuery : IRequest<List<SupplierOption>>
{
    public int? ProjectCategoryId { get; set; }
}

public record ProjectCategorySupplierOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCategorySupplierOptionQuery, List<SupplierOption>>
{
    public async Task<List<SupplierOption>> Handle(ProjectCategorySupplierOptionQuery query, CancellationToken cancellationToken)
    {
        if (!query.ProjectCategoryId.HasValue)
        {
            return [];
        }

        var queryable = Context.ProjectCategorySuppliers.AsQueryable().AsNoTracking().Where(e => e.ProjectCategoryId == query.ProjectCategoryId);

        return await queryable.Select(e => e.Supplier)
                              .ProjectTo<SupplierOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

    }
}