namespace Engage.Application.Services.SurveyFormEmployeeDivisions.Commands;

public record SurveyFormEmployeeDivisionBulkInsertCommand(int SurveyFormId, List<int> EmployeeDivisionIds) : IRequest<List<int>>
{
}

public class SurveyFormEmployeeDivisionBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyFormEmployeeDivisionBulkInsertCommand, List<int>>
{
    public SurveyFormEmployeeDivisionBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyFormEmployeeDivisionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormEmployeeDivisions.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EmployeeDivisionId).ToListAsync(cancellationToken);
        var insertIds = command.EmployeeDivisionIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.EmployeeDivisions.Where(e => insertIds.Contains(e.EmployeeDivisionId)).Select(e => e.EmployeeDivisionId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Divisions with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormEmployeeDivision { SurveyFormId = command.SurveyFormId, EmployeeDivisionId = id });
        _context.SurveyFormEmployeeDivisions.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormEmployeeDivisionBulkInsertValidator : AbstractValidator<SurveyFormEmployeeDivisionBulkInsertCommand>
{
    public SurveyFormEmployeeDivisionBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EmployeeDivisionIds).NotNull();
        RuleForEach(e => e.EmployeeDivisionIds).GreaterThan(0);
    }
}