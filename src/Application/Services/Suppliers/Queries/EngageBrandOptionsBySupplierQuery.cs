namespace Engage.Application.Services.Suppliers.Queries;

public class EngageBrandOptionsBySupplierQuery : IRequest<List<OptionDto>>
{
    public int SupplierId { get; set; }
}

public class EngageBrandOptionsBySupplierQueryHandler : IRequestHandler<EngageBrandOptionsBySupplierQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EngageBrandOptionsBySupplierQueryHandler(IAppDbContext context)
    {
        this._context = context;
    }

    public async Task<List<OptionDto>> Handle(EngageBrandOptionsBySupplierQuery request, CancellationToken cancellationToken)
    {
        return await _context.SupplierEngageBrands.Where(e => e.SupplierId == request.SupplierId &&
                                                              e.EngageBrand.Disabled == false)
                                                  .OrderBy(e => e.EngageBrand.Name)
                                                  .Select(e => new OptionDto(e.EngageBrandId, e.EngageBrand.Name))
                                                  .ToListAsync(cancellationToken);
    }
}
