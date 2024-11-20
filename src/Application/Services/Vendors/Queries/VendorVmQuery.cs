using Engage.Application.Services.Vendors.Models;

namespace Engage.Application.Services.Vendors.Queries;

public class GetVendorVmQuery : IRequest<VendorVm>
{
    public int Id { get; set; }
}

public class GetVendorVmQueryHandler : BaseQueryHandler, IRequestHandler<GetVendorVmQuery, VendorVm>
{
    public GetVendorVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<VendorVm> Handle(GetVendorVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Vendors.Include(e => e.Supplier)
                                           .Include(e => e.DistributionCenter)
                                           .SingleAsync(e => e.VendorId == request.Id, cancellationToken);

        return _mapper.Map<Vendor, VendorVm>(entity);
    }
}
