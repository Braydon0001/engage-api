namespace Engage.Application.Services.Users.Queries;

public class UserSupplierIdQuery : IRequest<int>
{
    public string Email { get; set; }
}

public class UserSupplierIdQueryHandler : IRequestHandler<UserSupplierIdQuery, int>
{
    private readonly IAppDbContext _context;

    public UserSupplierIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UserSupplierIdQuery query, CancellationToken cancellationToken)
    {

        return await _context.Users.Where(e => e.Email == query.Email)
                                   .Select(e => e.SupplierId)
                                   .FirstOrDefaultAsync(cancellationToken);

    }
}