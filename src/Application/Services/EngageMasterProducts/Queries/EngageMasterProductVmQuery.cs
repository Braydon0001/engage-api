using Engage.Application.Services.EngageMasterProducts.Models;

namespace Engage.Application.Services.EngageMasterProducts.Queries;

public class EngageMasterProductVmQuery : GetByIdQuery, IRequest<EngageMasterProductVm>
{
}

public class EngageMasterProductVmQueryHandler : BaseQueryHandler, IRequestHandler<EngageMasterProductVmQuery, EngageMasterProductVm>
{
    public EngageMasterProductVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageMasterProductVm> Handle(EngageMasterProductVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageMasterProducts.Include(x => x.Supplier)
                                                        .Include(x => x.ProductClassification)
                                                        .Include(x => x.EngageDepartment)
                                                        .Include(x => x.EngageBrand)
                                                        .Include(x => x.Vat)
                                                        .Include(x => x.EngageSubCategory)
                                                        .ThenInclude(x => x.EngageCategory)
                                                        .ThenInclude(x => x.EngageSubGroup)
                                                        .ThenInclude(x => x.EngageGroup)
                                                        .Include(x => x.EngageProductTags)
                                                        .ThenInclude(x => x.EngageTag)
                                                        .SingleAsync(x => x.EngageMasterProductId == request.Id, cancellationToken);

        return _mapper.Map<EngageMasterProduct, EngageMasterProductVm>(entity);
    }
}
