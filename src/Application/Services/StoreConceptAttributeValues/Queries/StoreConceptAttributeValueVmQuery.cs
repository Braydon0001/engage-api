using Engage.Application.Services.StoreConceptAttributeValues.Models;

namespace Engage.Application.Services.StoreConceptAttributeValues.Queries;

public class StoreConceptAttributeValueVmQuery : IRequest<StoreConceptAttributeValueVm>
{
    public int Id { get; set; }

}

public class StoreConceptAttributeValueVmQueryHandler : BaseQueryHandler, IRequestHandler<StoreConceptAttributeValueVmQuery, StoreConceptAttributeValueVm>
{
    public StoreConceptAttributeValueVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreConceptAttributeValueVm> Handle(StoreConceptAttributeValueVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreConceptAttributeValues.Include(x => x.Store)
                                                               .Include(x => x.Store)
                                                               .ThenInclude(y => y.StoreAssets)
                                                               .Include(x => x.StoreConceptAttribute)
                                                               .ThenInclude(y => y.StoreConceptAttributeOptions)
                                                               .Include(x => x.StoreConceptAttribute)
                                                               .ThenInclude(y => y.StoreConceptAttributeType)
                                                               .Include(x => x.StoreConceptAttribute)
                                                               .ThenInclude(y => y.StoreConceptAttributeStoreAssets)
                                                               .ThenInclude(z => z.StoreAsset)
                                                               .FirstOrDefaultAsync(x => x.StoreConceptAttributeValueId == request.Id, cancellationToken);

        return _mapper.Map<StoreConceptAttributeValue, StoreConceptAttributeValueVm>(entity);
    }
}
