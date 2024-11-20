// auto-generated
namespace Engage.Application.Services.WebFileEmployeeJobTitles.Commands;

public record WebFileEmployeeJobTitleBulkDeleteCommand(int WebFileId, List<int> EmployeeJobTitleIds) : IRequest<int?>
{
}

public class WebFileEmployeeJobTitleBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<WebFileEmployeeJobTitleBulkDeleteCommand, int?>
{
    public WebFileEmployeeJobTitleBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(WebFileEmployeeJobTitleBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.WebFileEmployeeJobTitles.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);
  
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

public class WebFileEmployeeJobTitleBulkDeleteValidator : AbstractValidator<WebFileEmployeeJobTitleBulkDeleteCommand>
{
    public WebFileEmployeeJobTitleBulkDeleteValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleIds).NotNull();
        RuleForEach(e => e.EmployeeJobTitleIds).GreaterThan(0);
    }
}
