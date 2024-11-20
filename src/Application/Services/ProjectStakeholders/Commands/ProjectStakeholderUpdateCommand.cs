namespace Engage.Application.Services.ProjectStakeholders.Commands;

public class ProjectStakeholderUpdateCommand : IRequest<OperationStatus>
{
    public int ProjectId { get; set; }
    public List<StakeholderIds> ProjectStakeholderIds { get; set; }
    public bool Save { get; set; } = true;
}

public record ProjectStakeholderUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectStakeholderUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStakeholderUpdateCommand command, CancellationToken cancellationToken)
    {
        var project = await Context.ProjectStores.FirstOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken)
            ?? throw new Exception("No Project Found");

        var stakeholders = await Context.ProjectStakeholders.Where(e => e.ProjectId == command.ProjectId).ToListAsync(cancellationToken);

        var assignees = await Context.ProjectAssignees.Where(e => e.ProjectId == command.ProjectId)
                                                      .Include(e => e.ProjectStakeholder)
                                                      .ToListAsync(cancellationToken);

        //Users

        var usersToRemove = stakeholders.OfType<ProjectStakeholderUser>()
                                                   .Where(e => !command.ProjectStakeholderIds.Where(f => f.Identifier == "user")
                                                                                             .Select(f => f.Id)
                                                                                             .Contains(e.UserId)).ToList();

        var usersToAdd = command.ProjectStakeholderIds.Where(e => e.Identifier == "user"
                                            && !stakeholders.OfType<ProjectStakeholderUser>()
                                                            .Select(f => f.UserId)
                                                            .Contains(e.Id)).ToList();

        if (usersToRemove.Count > 0)
        {
            foreach (var userToRemove in usersToRemove)
            {
                var assignee = assignees.FirstOrDefault(e => e.ProjectStakeholderId == userToRemove.ProjectStakeholderId);
                if (assignee == null)
                {
                    Context.ProjectStakeholderUsers.Remove(userToRemove);
                }
            }
        }

        if (usersToAdd.Count > 0)
        {
            Context.ProjectStakeholderUsers.AddRange(usersToAdd.Select(e => new ProjectStakeholderUser
            {
                ProjectId = command.ProjectId,
                UserId = e.Id
            }));
        }

        // External Users

        var externalUsersToRemove = stakeholders.OfType<ProjectStakeholderExternalUser>()
                                                   .Where(e => !command.ProjectStakeholderIds.Where(f => f.Identifier == "externalUser")
                                                                                             .Select(f => f.Id)
                                                                                             .Contains(e.ProjectExternalUserId)).ToList();

        var externalUsersToAdd = command.ProjectStakeholderIds.Where(e => e.Identifier == "externalUser"
                                            && !stakeholders.OfType<ProjectStakeholderExternalUser>()
                                                            .Select(f => f.ProjectExternalUserId)
                                                            .Contains(e.Id)).ToList();

        if (externalUsersToRemove.Count > 0)
        {
            foreach (var userToRemove in externalUsersToRemove)
            {
                var assignee = assignees.FirstOrDefault(e => e.ProjectStakeholderId == userToRemove.ProjectStakeholderId);
                if (assignee == null)
                {
                    Context.ProjectStakeholderExternalUsers.Remove(userToRemove);
                }
            }
        }

        if (externalUsersToAdd.Count > 0)
        {
            Context.ProjectStakeholderExternalUsers.AddRange(externalUsersToAdd.Select(e => new ProjectStakeholderExternalUser
            {
                ProjectId = command.ProjectId,
                ProjectExternalUserId = e.Id
            }));
        }

        // Store Contacts

        var storeContactsToRemove = stakeholders.OfType<ProjectStakeholderStoreContact>()
                                                   .Where(e => !command.ProjectStakeholderIds.Where(f => f.Identifier == "storeContact")
                                                                                             .Select(f => f.Id)
                                                                                             .Contains(e.StoreContactId)).ToList();

        var storeContactsToAdd = command.ProjectStakeholderIds.Where(e => e.Identifier == "storeContact"
                                            && !stakeholders.OfType<ProjectStakeholderStoreContact>()
                                                            .Select(f => f.StoreContactId)
                                                            .Contains(e.Id)).ToList();

        if (storeContactsToRemove.Count > 0)
        {
            foreach (var userToRemove in storeContactsToRemove)
            {
                var assignee = assignees.FirstOrDefault(e => e.ProjectStakeholderId == userToRemove.ProjectStakeholderId);
                if (assignee == null)
                {
                    Context.ProjectStakeholderStoreContacts.Remove(userToRemove);
                }
            }
        }

        if (storeContactsToAdd.Count > 0)
        {
            Context.ProjectStakeholderStoreContacts.AddRange(storeContactsToAdd.Select(e => new ProjectStakeholderStoreContact
            {
                ProjectId = command.ProjectId,
                StoreContactId = e.Id
            }));
        }

        // Supplier Contact

        var supplierContactsToRemove = stakeholders.OfType<ProjectStakeholderSupplierContact>()
                                                   .Where(e => !command.ProjectStakeholderIds.Where(f => f.Identifier == "supplierContact")
                                                                                             .Select(f => f.Id)
                                                                                             .Contains(e.SupplierContactId)).ToList();

        var supplierContactsToAdd = command.ProjectStakeholderIds.Where(e => e.Identifier == "supplierContact"
                                            && !stakeholders.OfType<ProjectStakeholderSupplierContact>()
                                                            .Select(f => f.SupplierContactId)
                                                            .Contains(e.Id)).ToList();

        if (supplierContactsToRemove.Count > 0)
        {
            foreach (var userToRemove in supplierContactsToRemove)
            {
                var assignee = assignees.FirstOrDefault(e => e.ProjectStakeholderId == userToRemove.ProjectStakeholderId);
                if (assignee == null)
                {
                    Context.ProjectStakeholderSupplierContacts.Remove(userToRemove);
                }
            }
        }

        if (supplierContactsToAdd.Count > 0)
        {
            Context.ProjectStakeholderSupplierContacts.AddRange(supplierContactsToAdd.Select(e => new ProjectStakeholderSupplierContact
            {
                ProjectId = command.ProjectId,
                SupplierContactId = e.Id
            }));
        }


        OperationStatus opStatus = new();

        if (command.Save)
        {
            opStatus = await Context.SaveChangesAsync(cancellationToken);
        }

        return opStatus;
    }
}

public class ProjectStakeholderUpdateValidator : AbstractValidator<ProjectStakeholderUpdateCommand>
{
    public ProjectStakeholderUpdateValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
    }
}