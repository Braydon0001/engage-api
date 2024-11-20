namespace Engage.Application.Services.ProjectStores.Commands;

public record ProjectStoreBulkInsertCommand(int ProjectId, List<int> StoreIds) : IRequest<List<int>>
{
}

public class ProjectStoreBulkInsertHandler : BulkInsertHandler, IRequestHandler<ProjectStoreBulkInsertCommand, List<int>>
{
    public ProjectStoreBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(ProjectStoreBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects.SingleOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.ProjectStores.IgnoreQueryFilters().Where(e => e.ProjectId == command.ProjectId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreId).ToListAsync(cancellationToken);
        var insertIds = command.StoreIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.Stores.Where(e => insertIds.Contains(e.StoreId)).Select(e => e.StoreId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Stores with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new ProjectStore { ProjectId = command.ProjectId, StoreId = id });
        _context.ProjectStores.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class ProjectStoreBulkInsertValidator : AbstractValidator<ProjectStoreBulkInsertCommand>
{
    public ProjectStoreBulkInsertValidator()
    {
        RuleFor(e => e.ProjectId).GreaterThan(0);
        RuleFor(e => e.StoreIds).NotNull();
        RuleForEach(e => e.StoreIds).GreaterThan(0);
    }
}