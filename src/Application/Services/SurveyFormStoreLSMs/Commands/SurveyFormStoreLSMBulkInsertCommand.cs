namespace Engage.Application.Services.SurveyFormStoreLSMs.Commands;

public record SurveyFormStoreLSMBulkInsertCommand(int SurveyFormId, List<int> StoreLSMIds) : IRequest<List<int>>
{
}

public class SurveyFormStoreLSMBulkInsertHandler : BulkInsertHandler, IRequestHandler<SurveyFormStoreLSMBulkInsertCommand, List<int>>
{
    public SurveyFormStoreLSMBulkInsertHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<List<int>> Handle(SurveyFormStoreLSMBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.SurveyFormId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.SurveyFormStoreLSMs.IgnoreQueryFilters().Where(e => e.SurveyFormId == command.SurveyFormId);

        // Calculate ids
        var currentIds = await queryable.Select(e => e.StoreLSMId).ToListAsync(cancellationToken);
        var insertIds = command.StoreLSMIds.Except(currentIds).ToList();

        // Insert Ids check
        var insertIdsCheck = await _context.StoreLSMs.Where(e => insertIds.Contains(e.Id)).Select(e => e.Id).ToListAsync(cancellationToken);
        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no Store LSMs with these ids: {string.Join(", ", notFoundIds)}");
        }

        // Bulk insert
        var entities = insertIds.Select(id => new SurveyFormStoreLSM { SurveyFormId = command.SurveyFormId, StoreLSMId = id });
        _context.SurveyFormStoreLSMs.AddRange(entities);

        await _context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class SurveyFormStoreLSMBulkInsertValidator : AbstractValidator<SurveyFormStoreLSMBulkInsertCommand>
{
    public SurveyFormStoreLSMBulkInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).GreaterThan(0);
        RuleFor(e => e.StoreLSMIds).NotNull();
        RuleForEach(e => e.StoreLSMIds).GreaterThan(0);
    }
}