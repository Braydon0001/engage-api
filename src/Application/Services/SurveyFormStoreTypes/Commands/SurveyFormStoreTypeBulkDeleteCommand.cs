namespace Engage.Application.Services.SurveyFormStoreTypes.Commands;

public record SurveyFormStoreTypeBulkDeleteCommand(int SurveyFormId, List<int> StoreTypeIds) : IRequest<int?>
{
}

public class SurveyFormStoreTypeBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<SurveyFormStoreTypeBulkDeleteCommand, int?>
{
    public SurveyFormStoreTypeBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(SurveyFormStoreTypeBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStoreTypes.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Delete Ids check
        var deleteIdsCheck = await _context.StoreTypes.Where(e => command.StoreTypeIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = command.StoreTypeIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Store Types with these ids: {string.Join(", ", notFoundIds)}");
        }

        return await queryable.Where(e => command.StoreTypeIds.Contains(e.StoreTypeId))
                              .ExecuteDeleteAsync(cancellationToken);
    }
}

public class SurveyFormStoreTypeBulkDeleteValidator : AbstractValidator<SurveyFormStoreTypeBulkDeleteCommand>
{
    public SurveyFormStoreTypeBulkDeleteValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreTypeIds).NotNull();
        RuleForEach(e => e.StoreTypeIds).GreaterThan(0);
    }
}
