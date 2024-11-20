namespace Engage.Application.Services.ProjectStakeholders.Commands;

public class ProjectStakeholderCreateCommand : IRequest<OperationStatus>
{
    public int ProjectId { get; set; }
    public List<StakeholderIds> ProjectStakeholderIds { get; set; }
    public List<StakeholderIds> ProjectAssignedIds { get; set; }
    public bool Save { get; set; } = true;
}

public class StakeholderIds
{
    public int Id { get; set; }
    public string Identifier { get; set; }
}

public record ProjectStakeholderCreateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectStakeholderCreateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStakeholderCreateCommand command, CancellationToken cancellationToken)
    {
        var project = await Context.ProjectStores.FirstOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken) ?? throw new Exception("No Project Found");

        var stakeholders = await Context.ProjectStakeholders.Where(e => e.ProjectId == command.ProjectId).ToListAsync(cancellationToken);

        var userStakeholders = stakeholders.OfType<ProjectStakeholderUser>().Select(e => e.UserId).ToList();
        var externalUserStakeholders = stakeholders.OfType<ProjectStakeholderExternalUser>().Select(e => e.ProjectExternalUserId).ToList();
        var storeContactStakeholders = stakeholders.OfType<ProjectStakeholderStoreContact>().Select(e => e.StoreContactId).ToList();
        var supplierContactStakeholders = stakeholders.OfType<ProjectStakeholderSupplierContact>().Select(e => e.SupplierContactId).ToList();
        var regionContacts = stakeholders.OfType<ProjectStakeholderEmployeeRegionContact>().Select(e => e.EmployeeRegionContactId).ToList();

        //users
        if (command.ProjectStakeholderIds.Any(e => e.Identifier == "user"))
        {
            var usersToAdd = command.ProjectStakeholderIds.Where(e => e.Identifier == "user"
                                                                && !userStakeholders.Contains(e.Id))
                                                              .Select(e => new ProjectStakeholderUser
                                                              {
                                                                  ProjectId = command.ProjectId,
                                                                  UserId = e.Id
                                                              })
                                                              .ToList();

            //var test = command.ProjectAssignedIds.Where(e => e.Identifier == "user")
            //                        .Select(e => new ProjectStakeholderUser
            //                        {
            //                            ProjectId = command.ProjectId,
            //                            UserId = e.Id
            //                        }).ToList();

            foreach (var assignee in command.ProjectAssignedIds.Where(e => e.Identifier == "user"))
            {
                var stakeholderUser = usersToAdd.FirstOrDefault(e => e.UserId == assignee.Id);
                if (stakeholderUser == null)
                {
                    usersToAdd.Add(new ProjectStakeholderUser
                    {
                        ProjectId = command.ProjectId,
                        UserId = assignee.Id
                    });
                }
            }

            //if (userStakeholders.Count > 0)
            //{
            //    usersToAdd = usersToAdd.Where(e => !userStakeholders.Contains(e.UserId)).ToList();
            //}

            Context.ProjectStakeholderUsers.AddRange(usersToAdd);
        }
        //externalUsers
        if (command.ProjectStakeholderIds.Any(e => e.Identifier == "externalUser"))
        {
            var externalsToAdd = command.ProjectStakeholderIds.Where(e => e.Identifier == "externalUser"
                                                                    && !externalUserStakeholders.Contains(e.Id))
                                                              .Select(e => new ProjectStakeholderExternalUser
                                                              {
                                                                  ProjectId = command.ProjectId,
                                                                  ProjectExternalUserId = e.Id
                                                              }).ToList();

            foreach (var assignee in command.ProjectAssignedIds.Where(e => e.Identifier == "externalUser"))
            {
                var stakeholderUser = externalsToAdd.FirstOrDefault(e => e.ProjectExternalUserId == assignee.Id);
                if (stakeholderUser == null)
                {
                    externalsToAdd.Add(new ProjectStakeholderExternalUser
                    {
                        ProjectId = command.ProjectId,
                        ProjectExternalUserId = assignee.Id
                    });
                }
            }

            Context.ProjectStakeholderExternalUsers.AddRange(externalsToAdd);
        }
        //storeContact
        if (command.ProjectStakeholderIds.Any(e => e.Identifier == "storeContact"))
        {
            var storeContactsToAdd = command.ProjectStakeholderIds.Where(e => e.Identifier == "storeContact"
                                                                    && !storeContactStakeholders.Contains(e.Id))
                                                                  .Select(e => new ProjectStakeholderStoreContact
                                                                  {
                                                                      ProjectId = command.ProjectId,
                                                                      StoreContactId = e.Id
                                                                  }).ToList();

            foreach (var assignee in command.ProjectAssignedIds.Where(e => e.Identifier == "storeContact"))
            {
                var stakeholderUser = storeContactsToAdd.FirstOrDefault(e => e.StoreContactId == assignee.Id);
                if (stakeholderUser == null)
                {
                    storeContactsToAdd.Add(new ProjectStakeholderStoreContact
                    {
                        ProjectId = command.ProjectId,
                        StoreContactId = assignee.Id
                    });
                }
            }

            Context.ProjectStakeholderStoreContacts.AddRange(storeContactsToAdd);
        }
        //supplierContact
        if (command.ProjectStakeholderIds.Any(e => e.Identifier == "supplierContact"))
        {
            var supplierContactsToAdd = command.ProjectStakeholderIds.Where(e => e.Identifier == "supplierContact"
                                                                    && !supplierContactStakeholders.Contains(e.Id))
                                                                  .Select(e => new ProjectStakeholderSupplierContact
                                                                  {
                                                                      ProjectId = command.ProjectId,
                                                                      SupplierContactId = e.Id
                                                                  }).ToList();

            foreach (var assignee in command.ProjectAssignedIds.Where(e => e.Identifier == "supplierContact"))
            {
                var stakeholderUser = supplierContactsToAdd.FirstOrDefault(e => e.SupplierContactId == assignee.Id);
                if (stakeholderUser == null)
                {
                    supplierContactsToAdd.Add(new ProjectStakeholderSupplierContact
                    {
                        ProjectId = command.ProjectId,
                        SupplierContactId = assignee.Id
                    });
                }
            }

            Context.ProjectStakeholderSupplierContacts.AddRange(supplierContactsToAdd);
        }
        //regionContact
        if (command.ProjectStakeholderIds.Any(e => e.Identifier == "regionContact"))
        {
            var regionContactsToAdd = command.ProjectStakeholderIds.Where(e => e.Identifier == "regionContact"
                                                                    && !regionContacts.Contains(e.Id))
                                                                  .Select(e => new ProjectStakeholderEmployeeRegionContact
                                                                  {
                                                                      ProjectId = command.ProjectId,
                                                                      EmployeeRegionContactId = e.Id
                                                                  }).ToList();

            foreach (var assignee in command.ProjectAssignedIds.Where(e => e.Identifier == "regionContact"))
            {
                var stakeholderUser = regionContactsToAdd.FirstOrDefault(e => e.EmployeeRegionContactId == assignee.Id);
                if (stakeholderUser == null)
                {
                    regionContactsToAdd.Add(new ProjectStakeholderEmployeeRegionContact
                    {
                        ProjectId = command.ProjectId,
                        EmployeeRegionContactId = assignee.Id
                    });
                }
            }

            Context.ProjectStakeholderEmployeeRegionContacts.AddRange(regionContactsToAdd);
        }

        var opStatus = new OperationStatus();

        if (command.Save)
        {
            opStatus = await Context.SaveChangesAsync(cancellationToken);
        }

        return opStatus;
    }
}

public class ProjectStakeholderCreateValidator : AbstractValidator<ProjectStakeholderCreateCommand>
{
    public ProjectStakeholderCreateValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectStakeholderIds).NotNull().NotEmpty();
    }
}