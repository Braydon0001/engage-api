// auto-generated
namespace Engage.Application.Services.CategoryFileEngageRegions.Commands;

public record CategoryFileEngageRegionBulkDeleteCommand(int CategoryFileId, List<int> EngageRegionIds) : IRequest<int?>
{
}

public class CategoryFileEngageRegionBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<CategoryFileEngageRegionBulkDeleteCommand, int?>
{
    public CategoryFileEngageRegionBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(CategoryFileEngageRegionBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileEngageRegions.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

        // Delete Ids check
        var deleteIdsCheck = await _context.EngageRegions.Where(e => command.EngageRegionIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = command.EngageRegionIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no EngageRegion with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EngageRegionIds.Contains(e.EngageRegionId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class CategoryFileEngageRegionBulkDeleteValidator : AbstractValidator<CategoryFileEngageRegionBulkDeleteCommand>
{
    public CategoryFileEngageRegionBulkDeleteValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.EngageRegionIds).NotNull();
        RuleForEach(e => e.EngageRegionIds).GreaterThan(0);
    }
}
