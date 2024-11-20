namespace Engage.Application.Services.Stores.Queries;

public class ClaimStoreOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ClaimId { get; set; }
}

public class ClaimStoreOptionsQueryHandler : IRequestHandler<ClaimStoreOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public ClaimStoreOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(ClaimStoreOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Claims.Where(e => e.ClaimId == request.ClaimId &&
                                                        e.Disabled == false
                                                    )
                                                   .OrderBy(e => e.Store.Name)
                                                   .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.StoreId, e.Store.Name))
                       .ToList();
    }
}