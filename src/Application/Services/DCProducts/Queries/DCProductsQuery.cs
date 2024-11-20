using Engage.Application.Services.DCProducts.Models;

namespace Engage.Application.Services.DCProducts.Queries;

public class DCProductsQuery : GetQuery, IRequest<ListResult<DCProductListDto>>
{
    public int VariantProductId { get; set; }
}

public class DCProductsQueryHandler : BaseQueryHandler, IRequestHandler<DCProductsQuery, ListResult<DCProductListDto>>
{
    public DCProductsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<DCProductListDto>> Handle(DCProductsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.DCProducts.Where(e => e.EngageVariantProductId == request.VariantProductId)
                                                .OrderBy(e => e.Name)
                                                .ProjectTo<DCProductListDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);

        return new ListResult<DCProductListDto>(entities);
    }
}
