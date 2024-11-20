// auto-generated
namespace Engage.Application.Services.ProductMasterColors.Queries;

public class ProductMasterColorVmQuery : IRequest<ProductMasterColorVm>
{
    public int Id { get; set; }
}

public class ProductMasterColorVmHandler : VmQueryHandler, IRequestHandler<ProductMasterColorVmQuery, ProductMasterColorVm>
{
    public ProductMasterColorVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterColorVm> Handle(ProductMasterColorVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterColors.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductMaster);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductMasterColorId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductMasterColorVm>(entity);
    }
}