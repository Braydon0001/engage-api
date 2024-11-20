namespace Engage.Application.Services.ProjectStores.Commands;

public record ProjectStoreBulkDeleteCommand(int ProjectId, List<int> StoreIds) : IRequest<int?>
{
}

public class ProjectStoreBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<ProjectStoreBulkDeleteCommand, int?>
{
    public ProjectStoreBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(ProjectStoreBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects.SingleOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.ProjectStores.IgnoreQueryFilters().Where(e => e.ProjectId == command.ProjectId);

        // Delete Ids check
        var deleteIdsCheck = await _context.Stores.Where(e => command.StoreIds.Contains(e.StoreId)).Select(e => e.StoreId).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Stores with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreIds.Contains(e.StoreId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class ProjectStoreBulkDeleteValidator : AbstractValidator<ProjectStoreBulkDeleteCommand>
{
    public ProjectStoreBulkDeleteValidator()
    {
        RuleFor(e => e.ProjectId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}
