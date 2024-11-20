namespace Engage.Application.CQRS.Commands;

public class BulkDelsertHandler
{
    protected readonly IAppDbContext _context;

    public BulkDelsertHandler(IAppDbContext context)
    {
        _context = context;
    }
}
