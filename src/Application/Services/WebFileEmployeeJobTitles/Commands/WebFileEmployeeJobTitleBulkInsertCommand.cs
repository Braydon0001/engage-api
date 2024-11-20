// auto-generated
namespace Engage.Application.Services.WebFileEmployeeJobTitles.Commands;

public record WebFileEmployeeJobTitleBulkInsertCommand(int WebFileId, List<int> EmployeeJobTitleIds) : IRequest<List<int>>
{
}

public class WebFileEmployeeJobTitleBulkInsertHandler : BulkInsertHandler, IRequestHandler<WebFileEmployeeJobTitleBulkInsertCommand, List<int>>
{
    public WebFileEmployeeJobTitleBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(WebFileEmployeeJobTitleBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.WebFileEmployeeJobTitles.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EmployeeJobTitleId).ToListAsync(cancellationToken);
        var insertIds = command.EmployeeJobTitleIds.Except(currentIds).ToList();
        
        // Insert Ids check
        var insertIdsCheck = await _context.EmployeeJobTitles.Where(e => insertIds.Contains(e.EmployeeJobTitleId)).Select(e => e.EmployeeJobTitleId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no EmployeeJobTitle with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new WebFileEmployeeJobTitle { WebFileId = command.WebFileId, EmployeeJobTitleId = id });
        _context.WebFileEmployeeJobTitles.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class WebFileEmployeeJobTitleBulkInsertValidator : AbstractValidator<WebFileEmployeeJobTitleBulkInsertCommand>
{
    public WebFileEmployeeJobTitleBulkInsertValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleIds).NotNull();
        RuleForEach(e => e.EmployeeJobTitleIds).GreaterThan(0);
    }
}