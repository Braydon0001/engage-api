namespace Engage.Application.Services.Stores.Queries;

public class IsStoreCodeQuery : IRequest<bool>
{
    public string Code { get; set; }
}

public class IsStoreCodeQueryHandler : IRequestHandler<IsStoreCodeQuery, bool>
{
    private readonly IAppDbContext _context;

    public IsStoreCodeQueryHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Handle(IsStoreCodeQuery request, CancellationToken cancellationToken)
    {
        return await _context.Stores.Where(e => e.Code == request.Code)
                                    .AnyAsync(cancellationToken);
    }
}
