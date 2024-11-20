using Engage.Application.Services.ProductAnalysisGroups.Models;

namespace Engage.Application.Services.ProductAnalysisGroups.Queries;

public class ProductAnalysisGroupsQuery : GetQuery, IRequest<ListResult<ProductAnalysisGroupDto>>
{

}

public class ProductAnalysisGroupsQueryHandler : BaseQueryHandler, IRequestHandler<ProductAnalysisGroupsQuery, ListResult<ProductAnalysisGroupDto>>
{
    public ProductAnalysisGroupsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ProductAnalysisGroupDto>> Handle(ProductAnalysisGroupsQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.ProductAnalysisGroups.OrderBy(e => e.Name)
                                                           .ProjectTo<ProductAnalysisGroupDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

        return new ListResult<ProductAnalysisGroupDto>(entities);
    }
}
