using Engage.Application.Services.Vendors.Models;

namespace Engage.Application.Services.Vendors.Queries;

public class VendorsQuery : GetQuery, IRequest<ListResult<VendorDto>>
{
    public int? DistributionCenterId { get; set; }
    public int? SupplierId { get; set; }
}

public class VendorsQueryHandler : BaseQueryHandler, IRequestHandler<VendorsQuery, ListResult<VendorDto>>
{
    public VendorsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<VendorDto>> Handle(VendorsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Vendors.AsQueryable();

        if (request.DistributionCenterId.HasValue)
        {
            queryable = queryable.Where(e => e.DistributionCenterId == request.DistributionCenterId);
        }
        
        if (request.SupplierId.HasValue)
        {
            queryable = queryable.Where(e => e.SupplierId == request.SupplierId);
        }

        var entities = await queryable.Where(e => e.SupplierId == request.SupplierId)
                                      .OrderBy(e => e.Name)
                                      .ProjectTo<VendorDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<VendorDto>(entities);
    }
}
