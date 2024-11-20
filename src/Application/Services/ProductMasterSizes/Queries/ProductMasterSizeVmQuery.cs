// auto-generated
namespace Engage.Application.Services.ProductMasterSizes.Queries;

public class ProductMasterSizeVmQuery : IRequest<ProductMasterSizeVm>
{
    public int Id { get; set; }
}

public class ProductMasterSizeVmHandler : VmQueryHandler, IRequestHandler<ProductMasterSizeVmQuery, ProductMasterSizeVm>
{
    public ProductMasterSizeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterSizeVm> Handle(ProductMasterSizeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterSizes.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductMaster);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductMasterSizeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductMasterSizeVm>(entity);
    }
}