using Engage.Application.Services.DCProducts.Models;

namespace Engage.Application.Services.DCProducts.Queries;

public class MultipleDCProductsQuery : IRequest<ListResult<VariantDCProductDto>>
{
    public int DistributionCenterId { get; set; }
    public List<int> EngageVariantProductIds { get; set; }
}

public class MultipleDCProductsQueryHandler : BaseQueryHandler, IRequestHandler<MultipleDCProductsQuery, ListResult<VariantDCProductDto>>
{
    public MultipleDCProductsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<VariantDCProductDto>> Handle(MultipleDCProductsQuery query, CancellationToken cancellationToken)
    {
        var multipleDCs = new List<VariantDCProductDto>();

        foreach (var id in query.EngageVariantProductIds)
        {
            var dcProducts = await _context.DCProducts.Where(e => e.Disabled == false && e.EngageVariantProduct.Disabled == false && e.EngageVariantProduct.EngageMasterProduct.Disabled == false && e.DistributionCenterId == query.DistributionCenterId &&
                                                                  e.EngageVariantProductId == id)
                                                      .Include(e => e.UnitType)
                                                      .ToListAsync(cancellationToken);
            if (dcProducts.Count > 1)
            {
                var dcProductDtos = dcProducts.Select(e => _mapper.Map<DCProduct, DCProductDto>(e))
                                              .ToList();

                multipleDCs.Add(new VariantDCProductDto
                {
                    EngageVariantProductId = id,
                    DCProducts = dcProductDtos
                });
            }
        }

        return new ListResult<VariantDCProductDto>(multipleDCs);
    }
}
