// auto-generated
namespace Engage.Application.Services.ProductModuleStatuses.Queries;

public class ProductModuleStatusVmQuery : IRequest<ProductModuleStatusVm>
{
    public int Id { get; set; }
}

public class ProductModuleStatusVmHandler : VmQueryHandler, IRequestHandler<ProductModuleStatusVmQuery, ProductModuleStatusVm>
{
    public ProductModuleStatusVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductModuleStatusVm> Handle(ProductModuleStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductModuleStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductModuleStatusId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductModuleStatusVm>(entity);
    }
}