namespace Engage.Application.Services.UserStores.Commands;

public record UserStoreDeleteCommand(int Id) : IRequest<UserStore>
{
}

public class UserStoreDeleteHandler : IRequestHandler<UserStoreDeleteCommand, UserStore>
{
    private readonly IAppDbContext _context;
    public UserStoreDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<UserStore> Handle(UserStoreDeleteCommand query, CancellationToken cancellationToken)
    {
        var entity = await _context.UserStores.SingleOrDefaultAsync(e => e.UserStoreId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        _context.UserStores.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
