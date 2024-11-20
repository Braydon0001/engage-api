// auto-generated
namespace Engage.Application.Services.SurveyEmployees.Commands;

public record SurveyEmployeeBulkInsertCommand(int SurveyId, List<int> EmployeeIds) : IRequest<List<int>>
{
}

public class SurveyEmployeeBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyEmployeeBulkInsertCommand, List<int>>
{
    public SurveyEmployeeBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyEmployeeBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleOrDefaultAsync(e => e.SurveyId == command.SurveyId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyEmployeeTargets.IgnoreQueryFilters().Where(e => e.SurveyId == command.SurveyId);

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
        var entities = insertIds.Select(id => new SurveyEmployeeTarget { SurveyId = command.SurveyId, EmployeeId = id });
        _context.SurveyEmployeeTargets.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyEmployeeBulkInsertValidator : AbstractValidator<SurveyEmployeeBulkInsertCommand>
{
    public SurveyEmployeeBulkInsertValidator()
    {
        RuleFor(e => e.SurveyId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}