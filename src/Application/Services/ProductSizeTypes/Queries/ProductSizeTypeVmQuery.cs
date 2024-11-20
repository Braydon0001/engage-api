// auto-generated
namespace Engage.Application.Services.ProductSizeTypes.Queries;

public class ProductSizeTypeVmQuery : IRequest<ProductSizeTypeVm>
{
    public int Id { get; set; }
}

public class ProductSizeTypeVmHandler : VmQueryHandler, IRequestHandler<ProductSizeTypeVmQuery, ProductSizeTypeVm>
{
    public ProductSizeTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSizeTypeVm> Handle(ProductSizeTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSizeTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductSizeTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductSizeTypeVm>(entity);
    }
}