namespace Engage.Application.Services.EmployeeRegionContacts.Commands;

public record EmployeeRegionContactDeleteCommand(int Id) : IRequest<EmployeeRegionContact>
{
}

public class EmployeeRegionContactDeleteHandler : IRequestHandler<EmployeeRegionContactDeleteCommand, EmployeeRegionContact>
{
    private readonly IAppDbContext _context;
    public EmployeeRegionContactDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeRegionContact> Handle(EmployeeRegionContactDeleteCommand query, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeRegionContacts.SingleOrDefaultAsync(e => e.EmployeeRegionContactId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        _context.EmployeeRegionContacts.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
