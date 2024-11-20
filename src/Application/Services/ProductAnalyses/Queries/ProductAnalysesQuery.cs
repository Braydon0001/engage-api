using Engage.Application.Services.ProductAnalyses.Models;

namespace Engage.Application.Services.ProductAnalyses.Queries;

public class ProductAnalysesQuery : GetQuery, IRequest<ListResult<ProductAnalysisDto>>
{
    public int? NewProp { get; set; }
}

public class ProductAnalysesQueryHandler : BaseQueryHandler, IRequestHandler<ProductAnalysesQuery, ListResult<ProductAnalysisDto>>
{
    public ProductAnalysesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ProductAnalysisDto>> Handle(ProductAnalysesQuery request, CancellationToken cancellationToken)
    {

        var queryable = _context.ProductAnalyses.AsQueryable();

        if (request.NewProp.HasValue)
        {
            queryable = queryable.Where(e => e.New == request.NewProp.Value);
        }

        var entities = await queryable.OrderByDescending(e => e.ProductAnalysisId)
                                      .ProjectTo<ProductAnalysisDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<ProductAnalysisDto>(entities);
    }
}
