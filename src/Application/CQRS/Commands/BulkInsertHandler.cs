namespace Engage.Application.CQRS.Commands;

public class BulkInsertHandler
{
    protected readonly IAppDbContext _context;

    public BulkInsertHandler(IAppDbContext context)
    {
        _context = context;
    }
}
