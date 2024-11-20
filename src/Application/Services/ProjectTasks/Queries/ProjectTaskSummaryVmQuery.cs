namespace Engage.Application.Services.ProjectTasks.Queries;

public record ProjectTaskSummaryVmQuery(int Id) : IRequest<ProjectTaskSummaryVm>;

public record ProjectTaskSummaryVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskSummaryVmQuery, ProjectTaskSummaryVm>
{
    public async Task<ProjectTaskSummaryVm> Handle(ProjectTaskSummaryVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTasks.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Project)
                             .Include(e => e.ProjectTaskType)
                             .Include(e => e.ProjectTaskStatus)
                             .Include(e => e.ProjectTaskPriority)
                             .Include(e => e.ProjectTaskProjectStakeholderUsers)
                                .ThenInclude(e => e.ProjectStakeholder)
                                    .ThenInclude(e => e.User)
                             //.Include(e => e.ProjectTaskSeverity)
                             .Include(e => e.ProjectStakeholder)
                                .ThenInclude(e => e.User);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectTaskId == query.Id, cancellationToken);

        if (entity == null)
        {
            return null;
        }

        var mappedEntity = entity == null ? null : Mapper.Map<ProjectTaskSummaryVm>(entity);

        //if (entity.ProjectStakeholderId.HasValue)
        //{
        //    //var regionContact = await Context.ProjectStakeholderEmployeeRegionContacts.Include(e => e.EmployeeRegionContact)
        //    //                                                                            .ThenInclude(e => e.Employee)
        //    //                                                                          .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

        //    //var storeContact = await Context.ProjectStakeholderStoreContacts.Include(e => e.StoreContact)
        //    //                                                                .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

        //    //var supplierContact = await Context.ProjectStakeholderSupplierContacts.Include(e => e.SupplierContact)
        //    //                                                                      .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

        //    var user = await Context.ProjectStakeholderUsers.Include(e => e.User)
        //                                                    .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

        //    //if (regionContact != null)
        //    //{
        //    //    mappedEntity.StakeholderId = new OptionDto
        //    //    {
        //    //        Id = regionContact.ProjectStakeholderId,
        //    //        Name = regionContact.EmployeeRegionContact.Employee.FirstName + " " + regionContact.EmployeeRegionContact.Employee.LastName + " - " + regionContact.EmployeeRegionContact.Employee.EmailAddress1 + "(Region Contact)",
        //    //    };
        //    //}

        //    //if (storeContact != null)
        //    //{
        //    //    mappedEntity.StakeholderId = new OptionDto
        //    //    {
        //    //        Id = storeContact.ProjectStakeholderId,
        //    //        Name = storeContact.StoreContact.FirstName + " " + storeContact.StoreContact.LastName + " - " + storeContact.StoreContact.EmailAddress1 + "(Store Contact)",
        //    //    };
        //    //}

        //    //if (supplierContact != null)
        //    //{
        //    //    mappedEntity.StakeholderId = new OptionDto
        //    //    {
        //    //        Id = supplierContact.ProjectStakeholderId,
        //    //        Name = supplierContact.SupplierContact.FirstName + " " + supplierContact.SupplierContact.LastName + " - " + supplierContact.SupplierContact.EmailAddress1 + "(Supplier Contact)",
        //    //    };
        //    //}

        //    if (user != null)
        //    {
        //        mappedEntity.ProjectStakeholderId = new OptionDto
        //        {
        //            Id = user.ProjectStakeholderId,
        //            Name = user.User.FirstName + " " + user.User.LastName + " - " + user.User.Email + "(User)",
        //        };
        //    }
        //}

        return mappedEntity;
    }
}