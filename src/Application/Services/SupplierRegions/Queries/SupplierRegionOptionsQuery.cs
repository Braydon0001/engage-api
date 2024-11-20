namespace Engage.Application.Services.SupplierRegions.Queries;

public class SupplierRegionOptionsQuery : IRequest<List<OptionDto>>
{
    public int SupplierId { get; set; }
}

public class SupplierRegionOptionsQueryHandler : IRequestHandler<SupplierRegionOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public SupplierRegionOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(SupplierRegionOptionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.SupplierRegions.Where(e => e.SupplierId == request.SupplierId)
                                             .Select(e => new OptionDto(e.Id, e.Name))
                                             .ToListAsync(cancellationToken);
    }
}
