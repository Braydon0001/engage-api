// auto-generated
namespace Engage.Application.Services.WebFileEmployees.Commands;

public record WebFileEmployeeBulkInsertCommand(int WebFileId, List<int> EmployeeIds) : IRequest<List<int>>
{
}

public class WebFileEmployeeBulkInsertHandler : BulkInsertHandler, IRequestHandler<WebFileEmployeeBulkInsertCommand, List<int>>
{
    public WebFileEmployeeBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(WebFileEmployeeBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.WebFileEmployees.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EmployeeId).ToListAsync(cancellationToken);
        var insertIds = command.EmployeeIds.Except(currentIds).ToList();
        
        // Insert Ids check
        var insertIdsCheck = await _context.Employees.Where(e => insertIds.Contains(e.EmployeeId)).Select(e => e.EmployeeId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no Employee with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new WebFileEmployee { WebFileId = command.WebFileId, EmployeeId = id });
        _context.WebFileEmployees.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class WebFileEmployeeBulkInsertValidator : AbstractValidator<WebFileEmployeeBulkInsertCommand>
{
    public WebFileEmployeeBulkInsertValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}