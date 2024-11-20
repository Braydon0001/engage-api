using Engage.Application.Services.ProductAnalysisDivisions.Models;

namespace Engage.Application.Services.ProductAnalysisDivisions.Queries;

public class ProductAnalysisDivisionVmQuery : GetByIdQuery, IRequest<ProductAnalysisDivisionVm>
{
}

public class ProductAnalysisDivisionVmHandler : BaseQueryHandler, IRequestHandler<ProductAnalysisDivisionVmQuery, ProductAnalysisDivisionVm>
{
    public ProductAnalysisDivisionVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductAnalysisDivisionVm> Handle(ProductAnalysisDivisionVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductAnalysisDivisions.SingleAsync(x => x.Id == request.Id, cancellationToken);

        return _mapper.Map<ProductAnalysisDivision, ProductAnalysisDivisionVm>(entity);
    }
}
