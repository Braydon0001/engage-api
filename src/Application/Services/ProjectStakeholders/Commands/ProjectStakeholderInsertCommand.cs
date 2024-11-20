namespace Engage.Application.Services.ProjectStakeholders.Commands;

public class ProjectStakeholderInsertCommand : IRequest<OperationStatus>
{
    public int ProjectId { get; set; }
    public List<int> UserIds { get; set; }
    public List<int> EmployeeRegionContactIds { get; set; }
    public List<int> StoreContactIds { get; set; }
    public List<int> SupplierContactIds { get; set; }
    public List<int> SupplierUserIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStakeholderInsertCommand, Project>();
    }
}

public record ProjectStakeholderInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectStakeholderInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStakeholderInsertCommand command, CancellationToken cancellationToken)
    {
        if (command.ProjectId < 1)
        {
            throw new Exception("Project not found");
        }

        var stakeholderUserIds = new List<int>();

        if (command.UserIds != null)
        {
            stakeholderUserIds.AddRange(command.UserIds);
        }

        if (command.SupplierUserIds != null)
        {
            stakeholderUserIds.AddRange(command.SupplierUserIds);
        }

        if (stakeholderUserIds != null)
        {
            if (stakeholderUserIds.Count > 0)
            {
                stakeholderUserIds = stakeholderUserIds.Distinct().ToList();

                var existingUserIds = await Context.ProjectStakeholderUsers.Where(e => e.ProjectId == command.ProjectId)
                                                                           .Select(e => e.UserId)
                                                                           .ToListAsync(cancellationToken);

                var userIds = stakeholderUserIds.Except(existingUserIds).ToList();

                foreach (var userId in userIds.Distinct())
                {
                    var projectStakeholderUser = new ProjectStakeholderUser
                    {
                        ProjectId = command.ProjectId,
                        UserId = userId
                    };

                    Context.ProjectStakeholderUsers.Add(projectStakeholderUser);
                }
            }
        }

        if (command.EmployeeRegionContactIds != null)
        {
            if (command.EmployeeRegionContactIds.Count > 0)
            {
                var existingEmployeeRegionContactIds = await Context.ProjectStakeholderEmployeeRegionContacts.Where(e => e.ProjectId == command.ProjectId)
                                                                                                             .Select(e => e.EmployeeRegionContactId)
                                                                                                             .ToListAsync();

                var employeeRegionContactIds = command.EmployeeRegionContactIds.Except(existingEmployeeRegionContactIds).ToList();

                foreach (var employeeRegionContactId in employeeRegionContactIds)
                {
                    var projectStakeholderEmployeeRegionContact = new ProjectStakeholderEmployeeRegionContact
                    {
                        ProjectId = command.ProjectId,
                        EmployeeRegionContactId = employeeRegionContactId
                    };

                    Context.ProjectStakeholderEmployeeRegionContacts.Add(projectStakeholderEmployeeRegionContact);
                }
            }
        }

        if (command.StoreContactIds != null)
        {
            if (command.StoreContactIds.Count > 0)
            {
                var existingStoreContactIds = await Context.ProjectStakeholderStoreContacts.Where(e => e.ProjectId == command.ProjectId)
                                                                                           .Select(e => e.StoreContactId)
                                                                                           .ToListAsync();

                var storeContactIds = command.StoreContactIds.Except(existingStoreContactIds).ToList();
                foreach (var storeContactId in storeContactIds)
                {
                    var projectStakeholderStoreContact = new ProjectStakeholderStoreContact
                    {
                        ProjectId = command.ProjectId,
                        StoreContactId = storeContactId
                    };

                    Context.ProjectStakeholderStoreContacts.Add(projectStakeholderStoreContact);
                }
            }
        }

        if (command.SupplierContactIds != null)
        {
            if (command.SupplierContactIds.Count > 0)
            {
                var existingSupplierContactIds = await Context.ProjectStakeholderSupplierContacts.Where(e => e.ProjectId == command.ProjectId)
                                                                                                 .Select(e => e.SupplierContactId)
                                                                                                 .ToListAsync();

                var supplierContactIds = command.SupplierContactIds.Except(existingSupplierContactIds).ToList();
                foreach (var supplierContactId in supplierContactIds)
                {
                    var projectStakeholderSupplierContact = new ProjectStakeholderSupplierContact
                    {
                        ProjectId = command.ProjectId,
                        SupplierContactId = supplierContactId
                    };

                    Context.ProjectStakeholderSupplierContacts.Add(projectStakeholderSupplierContact);
                }
            }
        }

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}

public class ProjectStakeholderInsertValidator : AbstractValidator<ProjectStakeholderInsertCommand>
{
    public ProjectStakeholderInsertValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
    }
}