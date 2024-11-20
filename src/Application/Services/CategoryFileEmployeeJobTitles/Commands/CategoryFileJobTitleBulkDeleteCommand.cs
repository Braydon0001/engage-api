// auto-generated
namespace Engage.Application.Services.CategoryFileEmployeeJobTitles.Commands;

public record CategoryFileEmployeeJobTitleBulkDeleteCommand(int CategoryFileId, List<int> EmployeeJobTitleIds) : IRequest<int?>
{
}

public class CategoryFileEmployeeJobTitleBulkDeleteHandler : BulkDeleteHandler, IRequestHandler<CategoryFileEmployeeJobTitleBulkDeleteCommand, int?>
{
    public CategoryFileEmployeeJobTitleBulkDeleteHandler(IAppDbContext context) : base(context)
    {
    }

    public async Task<int?> Handle(CategoryFileEmployeeJobTitleBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.CategoryFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = _context.CategoryFileEmployeeJobTitles.IgnoreQueryFilters().Where(e => e.CategoryFileId == command.CategoryFileId);

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

public class CategoryFileEmployeeJobTitleBulkDeleteValidator : AbstractValidator<CategoryFileEmployeeJobTitleBulkDeleteCommand>
{
    public CategoryFileEmployeeJobTitleBulkDeleteValidator()
    {
        RuleFor(e => e.CategoryFileId).GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleIds).NotNull();
        RuleForEach(e => e.EmployeeJobTitleIds).GreaterThan(0);
    }
}
