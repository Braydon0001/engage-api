namespace Engage.Application.Services.SurveyFormEmployeeJobTitles.Commands;

public record SurveyFormEmployeeJobTitleBulkInsertCommand(int SurveyFormId, List<int> EmployeeJobTitleIds) : IRequest<List<int>>
{
}

public class SurveyFormEmployeeJobTitleBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyFormEmployeeJobTitleBulkInsertCommand, List<int>>
{
    public SurveyFormEmployeeJobTitleBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyFormEmployeeJobTitleBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormEmployeeJobTitles.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EmployeeJobTitleId).ToListAsync(cancellationToken);
        var insertIds = command.EmployeeJobTitleIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.EmployeeJobTitles.Where(e => insertIds.Contains(e.EmployeeJobTitleId)).Select(e => e.EmployeeJobTitleId).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Job Titles with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormEmployeeJobTitle { SurveyFormId = command.SurveyFormId, EmployeeJobTitleId = id });
        _context.SurveyFormEmployeeJobTitles.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormEmployeeJobTitleBulkInsertValidator : AbstractValidator<SurveyFormEmployeeJobTitleBulkInsertCommand>
{
    public SurveyFormEmployeeJobTitleBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleIds).NotNull();
        RuleForEach(e => e.EmployeeJobTitleIds).GreaterThan(0);
    }
}