using Engage.Application.Services.ProductAnalyses.Models;

namespace Engage.Application.Services.ProductAnalyses.Queries;

public class ProductAnalysisVmQuery : GetByIdQuery, IRequest<ProductAnalysisVm>
{
}

public class ProductAnalysisVmHandler : BaseQueryHandler, IRequestHandler<ProductAnalysisVmQuery, ProductAnalysisVm>
{
    public ProductAnalysisVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductAnalysisVm> Handle(ProductAnalysisVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductAnalyses.Include(e => e.ProductAnalysisGroup)
                                                   .Include(e => e.ProductAnalysisDivision)
                                                   .Include(e => e.EngageGroup)
                                                   .Include(e => e.EngageSubGroup)
                                                   .Include(e => e.EngageCategory)
                                                   .Include(e => e.EngageSubCategory)
                                                   .Include(e => e.DistributionCenter)
                                                   .SingleAsync(x => x.ProductAnalysisId == request.Id, cancellationToken);

        return _mapper.Map<ProductAnalysis, ProductAnalysisVm>(entity);
    }
}
