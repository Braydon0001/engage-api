using Engage.Application.Services.ProductFilters.Models;

namespace Engage.Application.Services.ProductFilters.Queries;

public class ProductFilterVmQuery : IRequest<ProductFilterVm>
{
    public int Id { get; set; }
}

public class ProductFilterVmQueryHandler : BaseQueryHandler, IRequestHandler<ProductFilterVmQuery, ProductFilterVm>
{
    public ProductFilterVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductFilterVm> Handle(ProductFilterVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductFilters.Include(e => e.EngageVariantProduct)
                                                  .Include(e => e.FileUpload)
                                                  .SingleAsync(e => e.ProductFilterId == request.Id, cancellationToken);

        return _mapper.Map<ProductFilter, ProductFilterVm>(entity);
    }
}
