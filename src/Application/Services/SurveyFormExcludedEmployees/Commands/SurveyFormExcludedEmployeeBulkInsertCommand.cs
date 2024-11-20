namespace Engage.Application.Services.SurveyFormExcludedEmployees.Commands;

public class SurveyFormExcludedEmployeeBulkInsertCommand : IRequest<List<int>>
{
    public int SurveyFormId { get; set; }
    public List<int> EmployeeIds { get; set; }
}

public record SurveyFormExcludedEmployeeBulkInsertHandler(IAppDbContext Context) : IRequestHandler<SurveyFormExcludedEmployeeBulkInsertCommand, List<int>>
{
    public async Task<List<int>> Handle(SurveyFormExcludedEmployeeBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = Context.SurveyFormExcludedEmployees.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.ExcludedEmployeeId).ToListAsync(cancellationToken);
        var insertIds = command.EmployeeIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await Context.Employees.Where(e => insertIds.Contains(e.EmployeeId)).Select(e => e.EmployeeId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Employees with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormExcludedEmployee { SurveyFormId = command.SurveyFormId, ExcludedEmployeeId = id });
        Context.SurveyFormExcludedEmployees.AddRange(entities);

        //remove employees that are now excludded but were previously included
        var targetedEmployeesToBeRemoved = await Context.SurveyFormEmployees.IgnoreQueryFilters()
                                                                            .Where(e => e.SurveyFormId == command.SurveyFormId && command.EmployeeIds.Contains(e.EmployeeId))
                                                                            .ToListAsync(cancellationToken);

        if (targetedEmployeesToBeRemoved.Count > 0)
        {
            Context.SurveyFormEmployees.RemoveRange(targetedEmployeesToBeRemoved);
        }

        await Context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormExcludedEmployeeBulkInsertValidator : AbstractValidator<SurveyFormExcludedEmployeeBulkInsertCommand>
{
    public SurveyFormExcludedEmployeeBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EmployeeIds).NotNull();
        RuleForEach(e => e.EmployeeIds).GreaterThan(0);
    }
}