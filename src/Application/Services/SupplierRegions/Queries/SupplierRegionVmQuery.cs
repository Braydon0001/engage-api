using Engage.Application.Services.SupplierRegions.Models;

namespace Engage.Application.Services.SupplierRegions.Queries
{
    public class SupplierRegionVmQuery : GetByIdQuery, IRequest<SupplierRegionVm>
    {
    }

    public class SupplierRegionVMQueryHandler : BaseQueryHandler, IRequestHandler<SupplierRegionVmQuery, SupplierRegionVm>
    {
        public SupplierRegionVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<SupplierRegionVm> Handle(SupplierRegionVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.SupplierRegions.Include(e => e.Supplier)
                                                       .SingleAsync(e => e.Id == request.Id, cancellationToken);

            return _mapper.Map<SupplierRegion, SupplierRegionVm>(entity);
        }
    }
}
