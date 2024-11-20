using Engage.Application.Services.EngageVariantProducts.Models;

namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductVmQuery : GetByIdQuery, IRequest<EngageVariantProductVm>
{
}

public class EngageVariantProductVmQueryHandler : BaseQueryHandler, IRequestHandler<EngageVariantProductVmQuery, EngageVariantProductVm>
{
    public EngageVariantProductVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageVariantProductVm> Handle(EngageVariantProductVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageVariantProducts.Include(e => e.EngageMasterProduct)
                                                             .ThenInclude(mp => mp.Supplier)
                                                         .Include(e => e.EngageMasterProduct)
                                                             .ThenInclude(mp => mp.Vat)
                                                         .Include(e => e.EngageMasterProduct)
                                                             .ThenInclude(mp => mp.EngageBrand)
                                                         .Include(e => e.EngageMasterProduct)
                                                             .ThenInclude(mp => mp.EngageSubCategory)
                                                                 .ThenInclude(sc => sc.EngageCategory)
                                                                     .ThenInclude(ec => ec.EngageSubGroup)
                                                                         .ThenInclude(esg => esg.EngageGroup)
                                                         .Include(e => e.UnitType)
                                                         .SingleAsync(e => e.EngageVariantProductId == request.Id, cancellationToken);

        return _mapper.Map<EngageVariantProduct, EngageVariantProductVm>(entity);

    }
}
