// auto-generated
namespace Engage.Application.Services.SurveyEmployeeJobTitles.Commands;

public record SurveyEmployeeJobTitleBulkDeleteCommand(int SurveyId, List<int> EmployeeJobTitleIds) : IRequest<int?>
{
}

public class SurveyEmployeeJobTitleBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyEmployeeJobTitleBulkDeleteCommand, int?>
{
    public SurveyEmployeeJobTitleBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyEmployeeJobTitleBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleOrDefaultAsync(e => e.SurveyId == command.SurveyId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyEmployeeJobTitleTargets.IgnoreQueryFilters().Where(e => e.SurveyId == command.SurveyId);

        // Delete Ids check
        var deleteIdsCheck = await _context.EmployeeJobTitles.Where(e => command.EmployeeJobTitleIds.Contains(e.EmployeeJobTitleId)).Select(e => e.EmployeeJobTitleId).ToListAsync(cancellationToken);
        var notFoundIds = command.EmployeeJobTitleIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There is no EmployeeJobTitle with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.EmployeeJobTitleIds.Contains(e.EmployeeJobTitleId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyEmployeeJobTitleBulkDeleteValidator : AbstractValidator<SurveyEmployeeJobTitleBulkDeleteCommand>
{
    public SurveyEmployeeJobTitleBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyId).GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleIds).NotNull();
        RuleForEach(e => e.EmployeeJobTitleIds).GreaterThan(0);
    }
}
