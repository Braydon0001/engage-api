
namespace Engage.Application.Services.WebFileEmployeeDivisions.Commands;

public class WebFileEmployeeDivisionBulkInsertCommand : IRequest<List<int>>
{
    public int WebFileId { get; set; }
    public List<int> EmployeeDivisionIds { get; set; }
}

public record WebFileEmployeeDivisionBulkInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<WebFileEmployeeDivisionBulkInsertCommand, List<int>>
{
    public async Task<List<int>> Handle(WebFileEmployeeDivisionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var queryable = Context.WebFileEmployeeDivisions.IgnoreQueryFilters().Where(e => e.WebFileId == command.WebFileId);

        //calc ids
        var currenIds = await queryable.Select(e => e.EmployeeDivisionId).ToListAsync(cancellationToken);
        var insertIds = command.EmployeeDivisionIds.Except(currenIds).ToList();

        //insert Ids check
        var insertIdsCheck = await Context.EmployeeDivisions.Where(e => insertIds.Contains(e.EmployeeDivisionId)).Select(e => e.EmployeeDivisionId).ToListAsync(cancellationToken);

        var notFoundIds = insertIds.Except(insertIdsCheck).ToList();
        if (notFoundIds.Count > 0)
        {
            throw new Exception($"There are no EmployeeDivisions with these ids: {string.Join(", ", notFoundIds)}");
        }

        //Bulk insert
        var entities = insertIds.Select(id => new WebFileEmployeeDivision { WebFileId = command.WebFileId, EmployeeDivisionId = id });
        Context.WebFileEmployeeDivisions.AddRange(entities);

        await Context.SaveChangesAsync(cancellationToken);

        return insertIds;
    }
}

public class WebFileEmployeeDivisionBulkInsertValidator : AbstractValidator<WebFileEmployeeDivisionBulkInsertCommand>
{
    public WebFileEmployeeDivisionBulkInsertValidator()
    {
        RuleFor(e => e.WebFileId).GreaterThan(0);
        RuleFor(e => e.EmployeeDivisionIds).NotNull();
        RuleForEach(e => e.EmployeeDivisionIds).GreaterThan(0);
    }
}