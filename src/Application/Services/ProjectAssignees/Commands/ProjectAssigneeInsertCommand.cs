using Engage.Application.Services.ProjectStakeholders.Commands;

namespace Engage.Application.Services.ProjectAssignees.Commands;

public class ProjectAssigneeInsertCommand : IRequest<OperationStatus>
{
    public int ProjectId { get; init; }
    public List<StakeholderIds> ProjectAssigneeIds { get; init; }
    public bool Save { get; set; } = true;
}

public record ProjectAssigneeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectAssigneeInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectAssigneeInsertCommand command, CancellationToken cancellationToken)
    {
        var project = await Context.Projects.FirstOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken);
        if (project == null)
        {
            return null;
        }

        var stakeholders = await Context.ProjectStakeholders.Where(e => e.ProjectId == command.ProjectId).ToListAsync(cancellationToken);

        foreach (var item in command.ProjectAssigneeIds)
        {
            switch (item.Identifier)
            {
                case "user":
                    var stakeholderUser = stakeholders.OfType<ProjectStakeholderUser>().FirstOrDefault(e => e.UserId == item.Id);
                    if (stakeholderUser == null)
                    {
                        stakeholderUser = new ProjectStakeholderUser
                        {
                            UserId = item.Id,
                            ProjectId = command.ProjectId,
                        };
                        Context.ProjectStakeholderUsers.Add(stakeholderUser);

                    }
                    Context.ProjectAssignees.Add(new ProjectAssignee
                    {
                        ProjectId = command.ProjectId,
                        ProjectStakeholder = stakeholderUser
                    });
                    break;
                case "externalUser":
                    var stakeholderExternalUser = stakeholders.OfType<ProjectStakeholderExternalUser>().FirstOrDefault(e => e.ProjectExternalUserId == item.Id);
                    if (stakeholderExternalUser == null)
                    {
                        stakeholderExternalUser = new ProjectStakeholderExternalUser
                        {
                            ProjectId = command.ProjectId,
                            ProjectExternalUserId = item.Id,
                        };
                        Context.ProjectStakeholderExternalUsers.Add(stakeholderExternalUser);
                    }
                    Context.ProjectAssignees.Add(new ProjectAssignee
                    {
                        ProjectId = command.ProjectId,
                        ProjectStakeholder = stakeholderExternalUser
                    });
                    break;
                case "storeContact":
                    var stakeholderStoreContact = stakeholders.OfType<ProjectStakeholderStoreContact>().FirstOrDefault(e => e.StoreContactId == item.Id);
                    if (stakeholderStoreContact == null)
                    {
                        stakeholderStoreContact = new ProjectStakeholderStoreContact
                        {
                            ProjectId = command.ProjectId,
                            StoreContactId = item.Id,
                        };
                        Context.ProjectStakeholderStoreContacts.Add(stakeholderStoreContact);
                    }
                    Context.ProjectAssignees.Add(new ProjectAssignee
                    {
                        ProjectId = command.ProjectId,
                        ProjectStakeholder = stakeholderStoreContact
                    });
                    break;
                case "supplierContact":
                    var stakeholderExternalContact = stakeholders.OfType<ProjectStakeholderSupplierContact>().FirstOrDefault(e => e.SupplierContactId == item.Id);
                    if (stakeholderExternalContact == null)
                    {
                        stakeholderExternalContact = new ProjectStakeholderSupplierContact
                        {
                            ProjectId = command.ProjectId,
                            SupplierContactId = item.Id,
                        };
                        Context.ProjectStakeholderSupplierContacts.Add(stakeholderExternalContact);
                    }
                    Context.ProjectAssignees.Add(new ProjectAssignee
                    {
                        ProjectId = command.ProjectId,
                        ProjectStakeholder = stakeholderExternalContact
                    });
                    break;
            }
        }

        OperationStatus opStatus = new(true);

        if (command.Save)
        {
            opStatus = await Context.SaveChangesAsync(cancellationToken);
        }

        return opStatus;
    }
}

public class ProjectAssigneeInsertValidator : AbstractValidator<ProjectAssigneeInsertCommand>
{
    public ProjectAssigneeInsertValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectAssigneeIds).NotEmpty();
    }
}