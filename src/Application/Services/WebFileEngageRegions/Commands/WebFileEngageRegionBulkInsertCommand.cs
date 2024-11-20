// auto-generated
namespace Engage.Application.Services.WebFileEngageRegions.Commands;

public record WebFileEngageRegionBulkInsertCommand(int WebFileId, List<int> EngageRegionIds) : IRequest<List<int>>
{
}

public class WebFileEngageRegionBulkInsertHandler : BulkInsertHandler, IRequestHandler<WebFileEngageRegionBulkInsertCommand, List<int>>
{
    public WebFileEngageRegionBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(WebFileEngageRegionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.WebFileEngageRegions.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EngageRegionId).ToListAsync(cancellationToken);
        var insertIds = command.EngageRegionIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.EngageRegions.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no EngageRegion with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new WebFileEngageRegion { WebFileId = command.WebFileId, EngageRegionId = id });
        _context.WebFileEngageRegions.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class WebFileEngageRegionBulkInsertValidator : AbstractValidator<WebFileEngageRegionBulkInsertCommand>
{
    public WebFileEngageRegionBulkInsertValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.EngageRegionIds).NotNull();
        RuleForEach(e => e.EngageRegionIds).GreaterThan(0);
    }
}