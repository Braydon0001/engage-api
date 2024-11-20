using Engage.Application.Services.DCProducts.Models;

namespace Engage.Application.Services.DCProducts.Queries;

public class DCProductVmQuery : GetByIdQuery, IRequest<DCProductVm>
{
}

public class DCProductVmQueryHandler : BaseQueryHandler, IRequestHandler<DCProductVmQuery, DCProductVm>
{
    public DCProductVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<DCProductVm> Handle(DCProductVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.DCProducts.Include(e => e.EngageVariantProduct)
                                              .Include(e => e.DistributionCenter)
                                              .Include(e => e.ProductClass)
                                              .Include(e => e.Vendor)
                                              .Include(e => e.Manufacturer)
                                              .Include(e => e.UnitType)
                                              .Include(e => e.ProductActiveStatus)
                                              .Include(e => e.ProductStatus)
                                              .Include(e => e.ProductWarehouseStatus)
                                              .Include(e => e.ProductSubWarehouse)
                                              .SingleAsync(e => e.DCProductId == request.Id, cancellationToken);

        return _mapper.Map<DCProduct, DCProductVm>(entity);
    }
}
