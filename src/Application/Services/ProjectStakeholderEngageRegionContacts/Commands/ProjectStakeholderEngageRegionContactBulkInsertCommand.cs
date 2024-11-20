namespace Engage.Application.Services.ProjectStakeholderEngageRegionContacts.Commands;

public record ProjectStakeholderEngageRegionContactBulkInsertCommand(int ProjectId, List<int> StakeholderEngageRegionContactIds) : IRequest<List<int>>
{
}

public class ProjectStakeholderEngageRegionContactBulkInsertHandler : BulkInsertHandler, IRequestHandler<ProjectStakeholderEngageRegionContactBulkInsertCommand, List<int>>
{
    public ProjectStakeholderEngageRegionContactBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(ProjectStakeholderEngageRegionContactBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects.SingleOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.ProjectStakeholderEmployeeRegionContacts.IgnoreQueryFilters().Where(e => e.ProjectId == command.ProjectId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EmployeeRegionContactId).ToListAsync(cancellationToken);
        var insertIds = command.StakeholderEngageRegionContactIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.ProjectStakeholderEmployeeRegionContacts.Where(e => insertIds.Contains(e.EmployeeRegionContactId)).Select(e => e.EmployeeRegionContactId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no StakeholderEngageRegionContacts with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new ProjectStakeholderEmployeeRegionContact { ProjectId = command.ProjectId, EmployeeRegionContactId = id });
        _context.ProjectStakeholderEmployeeRegionContacts.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class ProjectStakeholderEngageRegionContactBulkInsertValidator : AbstractValidator<ProjectStakeholderEngageRegionContactBulkInsertCommand>
{
    public ProjectStakeholderEngageRegionContactBulkInsertValidator()
    {
        RuleFor(e => e.ProjectId).GreaterThan(0);
        RuleFor(e => e.StakeholderEngageRegionContactIds).NotNull();
        RuleForEach(e => e.StakeholderEngageRegionContactIds).GreaterThan(0);
    }
}