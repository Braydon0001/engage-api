namespace Engage.Application.Services.SurveyFormEmployeeEngageRegions.Commands;

public record SurveyFormEmployeeEngageRegionBulkDeleteCommand(int SurveyFormId, List<int> EmployeeEngageRegionIds) : IRequest<int?>
{
}

public class SurveyFormEmployeeEngageRegionBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyFormEmployeeEngageRegionBulkDeleteCommand, int?>
{
    public SurveyFormEmployeeEngageRegionBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyFormEmployeeEngageRegionBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormEmployeeEngageRegions.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await _context.EngageRegions.Where(e => command.EmployeeEngageRegionIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = command.EmployeeEngageRegionIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no regions with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EmployeeEngageRegionIds.Contains(e.EmployeeEngageRegionId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormEmployeeEngageRegionBulkDeleteValidator : AbstractValidator<SurveyFormEmployeeEngageRegionBulkDeleteCommand>
{
    public SurveyFormEmployeeEngageRegionBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.EmployeeEngageRegionIds).NotNull();
        RuleForEach(e => e.EmployeeEngageRegionIds).GreaterThan(0);
    }
}
