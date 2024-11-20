using Engage.Application.Services.EngageVariantProducts.Models;

namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductCatalogVmQuery : GetByIdQuery, IRequest<EngageVariantProductCatalogVm>
{
}

public class EngageVariantProductCatalogVmQueryHandler : BaseQueryHandler, IRequestHandler<EngageVariantProductCatalogVmQuery, EngageVariantProductCatalogVm>
{
    public EngageVariantProductCatalogVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageVariantProductCatalogVm> Handle(EngageVariantProductCatalogVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageVariantProducts.Include(e => e.EngageMasterProduct)
                                                            .ThenInclude(e => e.Supplier)
                                                         .Include(e => e.EngageMasterProduct)
                                                            .ThenInclude(e => e.Vat)
                                                         .Include(e => e.UnitType)
                                                         .SingleAsync(e => e.EngageVariantProductId == request.Id, cancellationToken);

        return _mapper.Map<EngageVariantProduct, EngageVariantProductCatalogVm>(entity);

    }
}
