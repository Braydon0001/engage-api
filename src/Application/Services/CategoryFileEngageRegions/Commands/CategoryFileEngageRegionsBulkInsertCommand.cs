
namespace Engage.Application.Services.CategoryFileEngageRegions.Commands;

public record CategoryFileEngageRegionBulkInsertCommand(int CategoryFileId, List<int> EngageRegionIds) : IRequest<List<int>>
{
}

public class CategoryFileEngageRegionBulkInsertHandler : BulkInsertHandler, IRequestHandler<CategoryFileEngageRegionBulkInsertCommand, List<int>>
{
    public CategoryFileEngageRegionBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(CategoryFileEngageRegionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileEngageRegions.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

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
        var entities = insertIds.Select(id => new CategoryFileEngageRegion { CategoryFileId = command.CategoryFileId, EngageRegionId = id });
        _context.CategoryFileEngageRegions.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class CategoryFileEngageRegionBulkInsertValidator : AbstractValidator<CategoryFileEngageRegionBulkInsertCommand>
{
    public CategoryFileEngageRegionBulkInsertValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.EngageRegionIds).NotNull();
        RuleForEach(e => e.EngageRegionIds).GreaterThan(0);
    }
}

