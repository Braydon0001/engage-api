namespace Engage.Application.Services.SurveyFormEngageDepartments.Commands;

public record SurveyFormEngageDepartmentBulkInsertCommand(int SurveyFormId, List<int> EngageDepartmentIds) : IRequest<List<int>>
{
}

public class SurveyFormEngageDepartmentBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyFormEngageDepartmentBulkInsertCommand, List<int>>
{
    public SurveyFormEngageDepartmentBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyFormEngageDepartmentBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormEngageDepartments.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EngageDepartmentId).ToListAsync(cancellationToken);
        var insertIds = command.EngageDepartmentIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.EngageDepartments.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Engage Departments with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormEngageDepartment { SurveyFormId = command.SurveyFormId, EngageDepartmentId = id });
        _context.SurveyFormEngageDepartments.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormEngageDepartmentBulkInsertValidator : AbstractValidator<SurveyFormEngageDepartmentBulkInsertCommand>
{
    public SurveyFormEngageDepartmentBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EngageDepartmentIds).NotNull();
        RuleForEach(e => e.EngageDepartmentIds).GreaterThan(0);
    }
}