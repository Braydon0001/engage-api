namespace Engage.Application.Services.WebFileEngageDepartments.Commands;

public class WebFileEngageDepartmentBulkInsertCommand : IRequest<List<int>>
{
    public int WebFileId { get; set; }
    public List<int> EngageDepartmentIds { get; set; }
}
public record WebFileEngageDepartmentBulkInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<WebFileEngageDepartmentBulkInsertCommand, List<int>>
{
    public async Task<List<int>> Handle(WebFileEngageDepartmentBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = Context.WebFileEngageDepartments.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);

        var currentIds = await queryable.Select(e => e.EngageDepartmentId).ToListAsync(cancellationToken);
        var insertIds = command.EngageDepartmentIds.Except(currentIds).ToList();

        var insertIdsCheck = await Context.EngageDepartments.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);

        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no EngageDepartments with these ids: {string.Join(", ", notFoundIds)}");
        }

        var entities = insertIds.Select(e => new WebFileEngageDepartment
        {
            WebFileId = command.WebFileId,
            EngageDepartmentId = e
        }).ToList();

        Context.WebFileEngageDepartments.AddRange(entities);

        await Context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class WebFileEngageDepartmentBulkInsertValidator : AbstractValidator<WebFileEngageDepartmentBulkInsertCommand>
{
    public WebFileEngageDepartmentBulkInsertValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.EngageDepartmentIds).NotNull();
        RuleForEach(e => e.EngageDepartmentIds).GreaterThan(0);
    }
}