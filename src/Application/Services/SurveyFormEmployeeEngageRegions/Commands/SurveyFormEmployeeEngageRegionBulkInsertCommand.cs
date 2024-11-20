namespace Engage.Application.Services.SurveyFormEmployeeEngageRegions.Commands;

public record SurveyFormEmployeeEngageRegionBulkInsertCommand(int SurveyFormId, List<int> EmployeeEngageRegionIds) : IRequest<List<int>>
{
}

public class SurveyFormEmployeeEngageRegionBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyFormEmployeeEngageRegionBulkInsertCommand, List<int>>
{
    public SurveyFormEmployeeEngageRegionBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyFormEmployeeEngageRegionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormEmployeeEngageRegions.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.EmployeeEngageRegionId).ToListAsync(cancellationToken);
        var insertIds = command.EmployeeEngageRegionIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.EngageRegions.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Regions with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormEmployeeEngageRegion { SurveyFormId = command.SurveyFormId, EmployeeEngageRegionId = id });
        _context.SurveyFormEmployeeEngageRegions.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormEmployeeEngageRegionBulkInsertValidator : AbstractValidator<SurveyFormEmployeeEngageRegionBulkInsertCommand>
{
    public SurveyFormEmployeeEngageRegionBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EmployeeEngageRegionIds).NotNull();
        RuleForEach(e => e.EmployeeEngageRegionIds).GreaterThan(0);
    }
}