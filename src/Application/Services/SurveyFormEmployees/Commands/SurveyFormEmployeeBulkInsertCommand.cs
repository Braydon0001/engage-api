namespace Engage.Application.Services.SurveyFormEmployees.Commands;

public record SurveyFormEmployeeBulkInsertCommand(int SurveyFormId, List<int> EmployeeIds) : IRequest<List<int>>
{
}

public class SurveyFormEmployeeBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyFormEmployeeBulkInsertCommand, List<int>>
{
    public SurveyFormEmployeeBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyFormEmployeeBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormEmployees.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EmployeeId).ToListAsync(cancellationToken);
        var insertIds = command.EmployeeIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.Employees.Where(e => insertIds.Contains(e.EmployeeId)).Select(e => e.EmployeeId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Employees with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormEmployee { SurveyFormId = command.SurveyFormId, EmployeeId = id });
        _context.SurveyFormEmployees.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormEmployeeBulkInsertValidator : AbstractValidator<SurveyFormEmployeeBulkInsertCommand>
{
    public SurveyFormEmployeeBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}