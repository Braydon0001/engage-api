namespace Engage.Application.Services.ProjectStakeholderEngageRegionContacts.Commands;

public record ProjectStakeholderEngageRegionContactBulkDeleteCommand(int ProjectId, List<int> StakeholderEngageRegionContactIds) : IRequest<int?>
{
}

public class ProjectStakeholderEngageRegionContactBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<ProjectStakeholderEngageRegionContactBulkDeleteCommand, int?>
{
    public ProjectStakeholderEngageRegionContactBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(ProjectStakeholderEngageRegionContactBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects.SingleOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.ProjectStakeholderEmployeeRegionContacts.IgnoreQueryFilters().Where(e => e.ProjectId == command.ProjectId);

        // Delete Ids check
        var deleteIdsCheck = await _context.ProjectStakeholderEmployeeRegionContacts.Where(e => command.StakeholderEngageRegionContactIds.Contains(e.EmployeeRegionContactId)).Select(e => e.EmployeeRegionContactId).ToListAsync(cancellationToken);
        var notFoundIds = command.StakeholderEngageRegionContactIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no StakeholderEngageRegionContacts with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StakeholderEngageRegionContactIds.Contains(e.EmployeeRegionContactId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class ProjectStakeholderEngageRegionContactBulkDeleteValidator : AbstractValidator<ProjectStakeholderEngageRegionContactBulkDeleteCommand>
{
    public ProjectStakeholderEngageRegionContactBulkDeleteValidator()
    {
        RuleFor(e => e.ProjectId).GreaterThan(0);
        RuleFor(e => e.StakeholderEngageRegionContactIds).NotNull();
        RuleForEach(e => e.StakeholderEngageRegionContactIds).GreaterThan(0);
    }
}
