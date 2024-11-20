namespace Engage.Application.Services.WebFileEngageDepartments.Commands;

public class WebFileEngageDepartmentBulkDeleteCommand : IRequest<int?>
{
    public int WebFileId { get; set; }
    public List<int> EngageDepartmentIds { get; set; }
}
public record WebFileEngageDepartmentBulkDeleteHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<WebFileEngageDepartmentBulkDeleteCommand, int?>
{
    public async Task<int?> Handle(WebFileEngageDepartmentBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = Context.WebFileEngageDepartments.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);

        var deleteIdsCheck = await Context.WebFileEngageDepartments.Where(e => command.EngageDepartmentIds.Contains(e.EngageDepartmentId))
                                                                   .Select(e => e.EngageDepartmentId).ToListAsync(cancellationToken);

        var notFoundIds = command.EngageDepartmentIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no EngageDepartments with these ids: {string.Join(", ", notFoundIds)}");
        }

        var entities = await queryable.Where(e => command.EngageDepartmentIds.Contains(e.EngageDepartmentId)).ToListAsync(cancellationToken);

        Context.WebFileEngageDepartments.RemoveRange(entities);

        await Context.SaveChangesAsync(cancellationToken);

        return entities.Count;
    }
}

public class WebFileEngageDepartmentBulkDeleteValidator : AbstractValidator<WebFileEngageDepartmentBulkDeleteCommand>
{
    public WebFileEngageDepartmentBulkDeleteValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.EngageDepartmentIds).NotNull();
        RuleForEach(e => e.EngageDepartmentIds).GreaterThan(0);
    }
}