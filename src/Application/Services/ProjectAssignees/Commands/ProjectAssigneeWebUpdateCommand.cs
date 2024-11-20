using Engage.Application.Services.ProjectStakeholders.Commands;

namespace Engage.Application.Services.ProjectAssignees.Commands;

public class ProjectAssigneeWebUpdateCommand : IRequest<OperationStatus>
{
    public int ProjectId { get; init; }
    public List<StakeholderIds> ProjectAssignedIds { get; set; }
    public bool Save { get; init; } = true;
}

public record ProjectAssigneeWebUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectAssigneeWebUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectAssigneeWebUpdateCommand command, CancellationToken cancellationToken)
    {
        var project = await Context.ProjectStores.FirstOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken)
            ?? throw new Exception("No Project Found");

        var assigned = await Context.ProjectAssignees.Where(e => e.ProjectId == command.ProjectId)
                                                     .Include(e => e.ProjectStakeholder)
                                                     .ToListAsync(cancellationToken);

        var stakeholders = await Context.ProjectStakeholders.Where(e => e.ProjectId == command.ProjectId).ToListAsync(cancellationToken);

        var assignedStakeholders = assigned.Select(e => e.ProjectStakeholder).ToList();

        //users
        var usersToDelete = assignedStakeholders.OfType<ProjectStakeholderUser>()
                                        .Where(e => !command.ProjectAssignedIds.Where(f => f.Identifier == "user").Select(f => f.Id).Contains(e.UserId))
                                        .ToList();

        var usersToAdd = command.ProjectAssignedIds.Where(e => e.Identifier == "user"
                            && !assignedStakeholders.OfType<ProjectStakeholderUser>().Select(e => e.UserId).Contains(e.Id))
                                                   .ToList();

        if (usersToDelete.Count > 0)
        {
            var userAssignedDelete = assigned.Where(e => usersToDelete.Select(e => e.ProjectStakeholderId).Contains(e.ProjectStakeholderId));
            Context.ProjectAssignees.RemoveRange(userAssignedDelete);
        }

        if (usersToAdd.Count > 0)
        {
            foreach (var user in usersToAdd)
            {
                var userStakeholder = stakeholders.OfType<ProjectStakeholderUser>().FirstOrDefault(e => e.UserId == user.Id);
                if (userStakeholder == null)
                {
                    userStakeholder = new ProjectStakeholderUser
                    {
                        UserId = user.Id,
                        ProjectId = command.ProjectId
                    };
                    Context.ProjectStakeholderUsers.Add(userStakeholder);
                }
                Context.ProjectAssignees.Add(new ProjectAssignee
                {
                    ProjectId = command.ProjectId,
                    ProjectStakeholder = userStakeholder
                });
            }

        }

        //external Users

        var externalUsersToDelete = assignedStakeholders.OfType<ProjectStakeholderExternalUser>()
                                        .Where(e => !command.ProjectAssignedIds.Where(f => f.Identifier == "externalUser").Select(f => f.Id).Contains(e.ProjectExternalUserId))
                                        .ToList();

        var externalUsersToAdd = command.ProjectAssignedIds.Where(e => e.Identifier == "externalUser"
                            && !assignedStakeholders.OfType<ProjectStakeholderExternalUser>().Select(e => e.ProjectExternalUserId).Contains(e.Id))
                                                   .ToList();

        if (externalUsersToDelete.Count > 0)
        {
            Context.ProjectAssignees.RemoveRange(assigned.Where(e => externalUsersToDelete.Select(f => f.ProjectStakeholderId).Contains(e.ProjectStakeholderId)));
        }

        if (externalUsersToAdd.Count > 0)
        {
            foreach (var user in externalUsersToAdd)
            {
                var externalUserStakeholder = stakeholders.OfType<ProjectStakeholderExternalUser>().FirstOrDefault(e => e.ProjectExternalUserId == user.Id);
                if (externalUserStakeholder == null)
                {
                    externalUserStakeholder = new ProjectStakeholderExternalUser
                    {
                        ProjectExternalUserId = user.Id,
                        ProjectId = command.ProjectId
                    };
                    Context.ProjectStakeholderExternalUsers.Add(externalUserStakeholder);
                }
                Context.ProjectAssignees.Add(new ProjectAssignee
                {
                    ProjectId = command.ProjectId,
                    ProjectStakeholder = externalUserStakeholder
                });
            }
        }

        // storeContacts

        var storeContactsToDelete = assignedStakeholders.OfType<ProjectStakeholderStoreContact>()
                                        .Where(e => !command.ProjectAssignedIds.Where(f => f.Identifier == "storeContact").Select(f => f.Id).Contains(e.StoreContactId))
                                        .ToList();

        var storeContactsToAdd = command.ProjectAssignedIds.Where(e => e.Identifier == "storeContact"
                            && !assignedStakeholders.OfType<ProjectStakeholderStoreContact>().Select(e => e.StoreContactId).Contains(e.Id))
                                                   .ToList();

        if (storeContactsToDelete.Count > 0)
        {
            Context.ProjectAssignees.RemoveRange(assigned.Where(e => storeContactsToDelete.Select(f => f.ProjectStakeholderId).Contains(e.ProjectStakeholderId)));
        }

        if (storeContactsToAdd.Count > 0)
        {
            foreach (var user in storeContactsToAdd)
            {
                var storeContactStakeholder = stakeholders.OfType<ProjectStakeholderStoreContact>().FirstOrDefault(e => e.StoreContactId == user.Id);
                if (storeContactStakeholder == null)
                {
                    storeContactStakeholder = new ProjectStakeholderStoreContact
                    {
                        StoreContactId = user.Id,
                        ProjectId = command.ProjectId
                    };
                    Context.ProjectStakeholderStoreContacts.Add(storeContactStakeholder);
                }
                Context.ProjectAssignees.Add(new ProjectAssignee
                {
                    ProjectId = command.ProjectId,
                    ProjectStakeholder = storeContactStakeholder
                });
            }
        }

        // Supplier Contacts

        var supplierContactsToDelete = assignedStakeholders.OfType<ProjectStakeholderSupplierContact>()
                                        .Where(e => !command.ProjectAssignedIds.Where(f => f.Identifier == "supplierContact").Select(f => f.Id).Contains(e.SupplierContactId))
                                        .ToList();

        var supplierContactsToAdd = command.ProjectAssignedIds.Where(e => e.Identifier == "supplierContact"
                            && !assignedStakeholders.OfType<ProjectStakeholderSupplierContact>().Select(e => e.SupplierContactId).Contains(e.Id))
                                                   .ToList();

        if (supplierContactsToDelete.Count > 0)
        {
            Context.ProjectAssignees.RemoveRange(assigned.Where(e => supplierContactsToDelete.Select(f => f.ProjectStakeholderId).Contains(e.ProjectStakeholderId)));
        }

        if (supplierContactsToAdd.Count > 0)
        {
            foreach (var user in supplierContactsToAdd)
            {
                var supplierContactStakeholder = stakeholders.OfType<ProjectStakeholderSupplierContact>().FirstOrDefault(e => e.SupplierContactId == user.Id);
                if (supplierContactStakeholder == null)
                {
                    supplierContactStakeholder = new ProjectStakeholderSupplierContact
                    {
                        SupplierContactId = user.Id,
                        ProjectId = command.ProjectId
                    };
                    Context.ProjectStakeholderSupplierContacts.Add(supplierContactStakeholder);
                }
                Context.ProjectAssignees.Add(new ProjectAssignee
                {
                    ProjectId = command.ProjectId,
                    ProjectStakeholder = supplierContactStakeholder
                });
            }
        }

        OperationStatus opStatus = new();

        if (command.Save)
        {
            opStatus = await Context.SaveChangesAsync(cancellationToken);
        }

        return opStatus;
    }
}

public class ProjectAssigneeWebUpdateValidator : AbstractValidator<ProjectAssigneeWebUpdateCommand>
{
    public ProjectAssigneeWebUpdateValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectAssignedIds).NotEmpty();
    }
}