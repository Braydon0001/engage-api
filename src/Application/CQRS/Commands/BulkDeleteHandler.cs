namespace Engage.Application.CQRS.Commands;

public class BulkDeleteHandler
{
    protected readonly IAppDbContext _context;

    public BulkDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }
}
