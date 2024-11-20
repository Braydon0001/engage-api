// auto-generated
namespace Engage.Application.Services.ProductPackSizeTypes.Queries;

public class ProductPackSizeTypeVmQuery : IRequest<ProductPackSizeTypeVm>
{
    public int Id { get; set; }
}

public class ProductPackSizeTypeVmHandler : VmQueryHandler, IRequestHandler<ProductPackSizeTypeVmQuery, ProductPackSizeTypeVm>
{
    public ProductPackSizeTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductPackSizeTypeVm> Handle(ProductPackSizeTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductPackSizeTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductPackSizeTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductPackSizeTypeVm>(entity);
    }
}