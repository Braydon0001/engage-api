using Engage.Application.Services.ProductAnalysisGroups.Models;

namespace Engage.Application.Services.ProductAnalysisGroups.Queries;

public class ProductAnalysisGroupVmQuery : GetByIdQuery, IRequest<ProductAnalysisGroupVm>
{
}

public class ProductAnalysisGroupVmHandler : BaseQueryHandler, IRequestHandler<ProductAnalysisGroupVmQuery, ProductAnalysisGroupVm>
{
    public ProductAnalysisGroupVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductAnalysisGroupVm> Handle(ProductAnalysisGroupVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductAnalysisGroups.SingleAsync(x => x.Id == request.Id, cancellationToken);

        return _mapper.Map<ProductAnalysisGroup, ProductAnalysisGroupVm>(entity);
    }
}
