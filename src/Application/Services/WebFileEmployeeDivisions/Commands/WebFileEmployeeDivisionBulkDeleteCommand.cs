namespace Engage.Application.Services.WebFileEmployeeDivisions.Commands;

public class WebFileEmployeeDivisionBulkDeleteCommand : IRequest<int?>
{
    public int WebFileId { get; set; }
    public List<int> EmployeeDivisionIds { get; set; }
}
public record WebFileEmployeeDivisionBulkDeleteHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<WebFileEmployeeDivisionBulkDeleteCommand, int?>
{
    public async Task<int?> Handle(WebFileEmployeeDivisionBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
            return null;

        var queryable = Context.WebFileEmployeeDivisions.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);

        var deleteIdsCheck = await Context.EmployeeDivisions.Where(e => command.EmployeeDivisionIds.Contains(e.EmployeeDivisionId))
                                                            .Select(e => e.EmployeeDivisionId).ToListAsync(cancellationToken);
        var notFoundIds = command.EmployeeDivisionIds.Except(deleteIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no EmployeeDivisions with these ids: {string.Join(", ", notFoundIds)}");
        }

        var entities = await queryable.Where(e => command.EmployeeDivisionIds.Contains(e.EmployeeDivisionId)).ToListAsync(cancellationToken);

        Context.WebFileEmployeeDivisions.RemoveRange(entities);

        await Context.SaveChangesAsync(cancellationToken);

        return entities.Count;

        //return await queryable.Where(e => command.EmployeeDivisionIds.Contains(e.EmployeeDivisionId)).ExecuteDeleteAsync(cancellationToken);
    }
}
public class WebFileEmployeeDivisionBulkDeleteValidator : AbstractValidator<WebFileEmployeeDivisionBulkDeleteCommand>
{
    public WebFileEmployeeDivisionBulkDeleteValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.EmployeeDivisionIds).NotEmpty();
        RuleForEach(e => e.EmployeeDivisionIds).GreaterThan(0);
    }
}