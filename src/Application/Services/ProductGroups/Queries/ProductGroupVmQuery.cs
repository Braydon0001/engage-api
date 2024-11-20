// auto-generated
namespace Engage.Application.Services.ProductGroups.Queries;

public class ProductGroupVmQuery : IRequest<ProductGroupVm>
{
    public int Id { get; set; }
}

public class ProductGroupVmHandler : VmQueryHandler, IRequestHandler<ProductGroupVmQuery, ProductGroupVm>
{
    public ProductGroupVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductGroupVm> Handle(ProductGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductGroups.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductGroupId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductGroupVm>(entity);
    }
}