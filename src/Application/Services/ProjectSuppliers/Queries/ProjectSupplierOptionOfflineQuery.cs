using Engage.Application.Services.Suppliers.Queries;

namespace Engage.Application.Services.ProjectSuppliers.Queries;

public class ProjectSupplierOptionOfflineQuery : IRequest<List<SupplierOption>>
{
    public int? UserId { get; set; }
}

public record ProjectSupplierOptionOfflineHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSupplierOptionOfflineQuery, List<SupplierOption>>
{
    public async Task<List<SupplierOption>> Handle(ProjectSupplierOptionOfflineQuery query, CancellationToken cancellationToken)
    {
        var userStakeholderIds = await Context.ProjectStakeholderUsers.Where(e => e.UserId == query.UserId && e.Disabled != true).Select(e => e.ProjectStakeholderId).ToListAsync(cancellationToken);





        var projectIds = await Context.ProjectAssignees.AsNoTracking()
                                                       .Where(e => userStakeholderIds.Contains(e.ProjectStakeholderId))
                                                       .Include(e => e.ProjectStakeholder)
                                                       .Select(e => e.ProjectId)
                                                       .ToListAsync(cancellationToken);

        var ownerProjectIds = await Context.ProjectStores.Where(e => e.OwnerId == query.UserId).Select(e => e.ProjectId).ToListAsync(cancellationToken);


        projectIds.AddRange(ownerProjectIds);

        var supplierIds = await Context.ProjectSuppliers.AsNoTracking()
                                                             .Where(e => projectIds.Contains(e.ProjectId))
                                                             .Select(e => e.SupplierId)
                                                             .ToListAsync(cancellationToken);

        var queryable = Context.ProjectCategorySuppliers.AsQueryable().AsNoTracking().Where(e => supplierIds.Contains(e.SupplierId));

        return await queryable.Select(e => e.Supplier)
                               .Distinct()
                              .ProjectTo<SupplierOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

    }
}