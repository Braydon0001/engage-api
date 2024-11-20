namespace Engage.Application.Services.ProjectProjectTags.Commands;

public record ProjectProjectTagDeleteCommand(int Id) : IRequest<ProjectProjectTag>
{
}

public class ProjectProjectTagDeleteHandler : IRequestHandler<ProjectProjectTagDeleteCommand, ProjectProjectTag>
{
    private readonly IAppDbContext _context;
    public ProjectProjectTagDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectProjectTag> Handle(ProjectProjectTagDeleteCommand query, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectProjectTags.SingleOrDefaultAsync(e => e.ProjectProjectTagId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var projectTagRegion = await _context.ProjectProjectTagEngageRegions.Where(x => x.ProjectProjectTagId == query.Id)
                                                                            .FirstOrDefaultAsync(cancellationToken);

        var projectTagSupplier = await _context.ProjectProjectTagSuppliers.Where(x => x.ProjectProjectTagId == query.Id)
                                                                          .FirstOrDefaultAsync(cancellationToken);

        var projectTagStore = await _context.ProjectProjectTagStores.Where(x => x.ProjectProjectTagId == query.Id)
                                                                    .FirstOrDefaultAsync(cancellationToken);

        if (projectTagRegion != null)
        {
            var regionContactIds = await _context.EmployeeRegionContacts.Where(x => x.EngageRegionId == projectTagRegion.EngageRegionId)
                                                                      .Select(x => x.EmployeeRegionContactId)
                                                                      .ToListAsync(cancellationToken);

            var stakeholders = await _context.ProjectStakeholderEmployeeRegionContacts.Where(x => x.ProjectId == projectTagRegion.ProjectId && regionContactIds.Contains(x.EmployeeRegionContactId))
                                                                                      .ToListAsync(cancellationToken);

            _context.ProjectStakeholderEmployeeRegionContacts.RemoveRange(stakeholders);
        }

        if (projectTagSupplier != null)
        {
            var supplierContactIds = await _context.SupplierContacts.Where(x => x.SupplierId == projectTagSupplier.SupplierId)
                                                                    .Select(x => x.EntityContactId)
                                                                    .ToListAsync(cancellationToken);

            var stakeholders = await _context.ProjectStakeholderSupplierContacts.Where(x => x.ProjectId == projectTagSupplier.ProjectId && supplierContactIds.Contains(x.SupplierContactId))
                                                                                .ToListAsync(cancellationToken);

            _context.ProjectStakeholderSupplierContacts.RemoveRange(stakeholders);

            var supplierUserIds = await _context.Users.Where(x => x.SupplierId == projectTagSupplier.SupplierId)
                                                      .Select(x => x.UserId)
                                                      .ToListAsync(cancellationToken);

            var supplierUserStakeholders = await _context.ProjectStakeholderUsers.Where(x => x.ProjectId == projectTagSupplier.ProjectId && supplierUserIds.Contains(x.UserId))
                                                                                 .ToListAsync(cancellationToken);

            _context.ProjectStakeholderUsers.RemoveRange(supplierUserStakeholders);
        }

        if (projectTagStore != null)
        {
            var projectStore = await _context.ProjectStores.Where(x => x.ProjectId == projectTagStore.ProjectId)
                                                           .FirstOrDefaultAsync(cancellationToken);

            if (projectStore != null)
            {
                if (projectStore.StoreId != projectTagStore.StoreId)
                {
                    var storeContactIds = await _context.StoreContacts.Where(x => x.StoreId == projectTagStore.StoreId)
                                                                      .Select(x => x.EntityContactId)
                                                                      .ToListAsync(cancellationToken);

                    var stakeholders = await _context.ProjectStakeholderStoreContacts.Where(x => x.ProjectId == projectTagStore.ProjectId && storeContactIds.Contains(x.StoreContactId))
                                                                                     .ToListAsync(cancellationToken);

                    _context.ProjectStakeholderStoreContacts.RemoveRange(stakeholders);

                    var storeTagUserIds = await _context.EmployeeStores.Where(x => x.StoreId == projectTagStore.StoreId)
                                                              .Select(x => x.Employee.UserId)
                                                              .ToListAsync(cancellationToken);

                    var storeUserIds = await _context.EmployeeStores.Where(x => x.StoreId == projectStore.StoreId)
                                                                    .Select(x => x.Employee.UserId)
                                                                    .ToListAsync(cancellationToken);

                    storeTagUserIds = storeTagUserIds.Except(storeUserIds).ToList();

                    var taskUserIds = await _context.ProjectTasks.Where(x => x.ProjectId == projectTagStore.ProjectId)
                                                                 .Select(x => x.UserId)
                                                                 .ToListAsync(cancellationToken);

                    storeTagUserIds = storeTagUserIds.Except(taskUserIds).ToList();
                    storeTagUserIds = storeTagUserIds.Distinct().ToList();

                    var storeUserStakeholders = await _context.ProjectStakeholderUsers.Where(x => x.ProjectId == projectTagStore.ProjectId && storeTagUserIds.Contains(x.UserId))
                                                                                      .ToListAsync(cancellationToken);

                    _context.ProjectStakeholderUsers.RemoveRange(storeUserStakeholders);
                }
            }
        }

        _context.ProjectProjectTags.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
