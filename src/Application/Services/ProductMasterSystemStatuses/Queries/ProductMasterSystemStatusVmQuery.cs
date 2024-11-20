// auto-generated
namespace Engage.Application.Services.ProductMasterSystemStatuses.Queries;

public class ProductMasterSystemStatusVmQuery : IRequest<ProductMasterSystemStatusVm>
{
    public int Id { get; set; }
}

public class ProductMasterSystemStatusVmHandler : VmQueryHandler, IRequestHandler<ProductMasterSystemStatusVmQuery, ProductMasterSystemStatusVm>
{
    public ProductMasterSystemStatusVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterSystemStatusVm> Handle(ProductMasterSystemStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterSystemStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductMasterSystemStatusId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductMasterSystemStatusVm>(entity);
    }
}