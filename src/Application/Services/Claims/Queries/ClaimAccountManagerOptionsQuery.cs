namespace Engage.Application.Services.Claims.Queries;

public class ClaimAccountManagerOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int SupplierId { get; set; }
}

public class ClaimAccountManagerOptionsQueryHandler : IRequestHandler<ClaimAccountManagerOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public ClaimAccountManagerOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(ClaimAccountManagerOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.SupplierClaimAccountManagers.Include(e => e.User)
                                                                  .Where(e => e.SupplierId == request.SupplierId && e.User.Disabled == false)
                                                                  .OrderBy(e => e.User.FullName)
                                                                  .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.UserId, e.User.FullName))
                       .ToList();
    }
}
