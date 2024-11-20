using Engage.Application.Services.SupplierRegions.Models;

namespace Engage.Application.Services.SupplierRegions.Queries;

public class SupplierRegionsQuery : GetQuery, IRequest<ListResult<SupplierRegionDto>>
{
    public int SupplierId { get; set; }
}

public class SupplierRegionsQueryHandler : BaseQueryHandler, IRequestHandler<SupplierRegionsQuery, ListResult<SupplierRegionDto>>
{
    public SupplierRegionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<SupplierRegionDto>> Handle(SupplierRegionsQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.SupplierRegions.Where(e => e.SupplierId == request.SupplierId && e.Deleted == false)
                                                     .OrderBy(e => e.Id)
                                                     .ProjectTo<SupplierRegionDto>(_mapper.ConfigurationProvider)
                                                     .ToListAsync(cancellationToken);

        return new ListResult<SupplierRegionDto>(entities);
    }
} 
