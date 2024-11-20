// auto-generated
namespace Engage.Application.Services.SurveyEmployeeJobTitles.Commands;

public record SurveyEmployeeJobTitleBulkInsertCommand(int SurveyId, List<int> EmployeeJobTitleIds) : IRequest<List<int>>
{
}

public class SurveyEmployeeJobTitleBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyEmployeeJobTitleBulkInsertCommand, List<int>>
{
    public SurveyEmployeeJobTitleBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyEmployeeJobTitleBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleOrDefaultAsync(e => e.SurveyId == command.SurveyId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyEmployeeJobTitleTargets.IgnoreQueryFilters().Where(e => e.SurveyId == command.SurveyId);

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
        var entities = insertIds.Select(id => new SurveyEmployeeJobTitleTarget { SurveyId = command.SurveyId, EmployeeJobTitleId = id });
        _context.SurveyEmployeeJobTitleTargets.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyEmployeeJobTitleBulkInsertValidator : AbstractValidator<SurveyEmployeeJobTitleBulkInsertCommand>
{
    public SurveyEmployeeJobTitleBulkInsertValidator()
    {
        RuleFor(e => e.SurveyId).GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleIds).NotNull();
        RuleForEach(e => e.EmployeeJobTitleIds).GreaterThan(0);
    }
}