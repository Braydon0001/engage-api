// auto-generated
namespace Engage.Application.Services.ProductMasterStatuses.Queries;

public class ProductMasterStatusVmQuery : IRequest<ProductMasterStatusVm>
{
    public int Id { get; set; }
}

public class ProductMasterStatusVmHandler : VmQueryHandler, IRequestHandler<ProductMasterStatusVmQuery, ProductMasterStatusVm>
{
    public ProductMasterStatusVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterStatusVm> Handle(ProductMasterStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductMasterStatusId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductMasterStatusVm>(entity);
    }
}