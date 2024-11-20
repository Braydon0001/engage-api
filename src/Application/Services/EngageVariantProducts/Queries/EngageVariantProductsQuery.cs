using Engage.Application.Services.EngageVariantProducts.Models;

namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductsQuery : GetQuery, IRequest<ListResult<EngageVariantProductListDto>>
{
    public int MasterProductId { get; set; }
}

public class EngageVariantProductsQueryHandler : BaseQueryHandler, IRequestHandler<EngageVariantProductsQuery, ListResult<EngageVariantProductListDto>>
{
    public EngageVariantProductsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<EngageVariantProductListDto>> Handle(EngageVariantProductsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EngageVariantProducts.Where(e => e.EngageMasterProductId == request.MasterProductId)
                                                           .OrderBy(e => e.Name)
                                                           .ProjectTo<EngageVariantProductListDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

        return new ListResult<EngageVariantProductListDto>(entities);
    }
}
