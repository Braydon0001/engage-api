using Engage.Application.Services.ProductAnalysisDivisions.Models;

namespace Engage.Application.Services.ProductAnalysisDivisions.Queries;

public class ProductAnalysisDivisionsQuery : GetQuery, IRequest<ListResult<ProductAnalysisDivisionDto>>
{

}

public class ProductAnalysisDivisionsQueryHandler : BaseQueryHandler, IRequestHandler<ProductAnalysisDivisionsQuery, ListResult<ProductAnalysisDivisionDto>>
{
    public ProductAnalysisDivisionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ProductAnalysisDivisionDto>> Handle(ProductAnalysisDivisionsQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.ProductAnalysisDivisions.OrderBy(e => e.Name)
                                                           .ProjectTo<ProductAnalysisDivisionDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

        return new ListResult<ProductAnalysisDivisionDto>(entities);
    }
}
