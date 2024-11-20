// auto-generated
namespace Engage.Application.Services.ProductSubGroups.Queries;

public class ProductSubGroupVmQuery : IRequest<ProductSubGroupVm>
{
    public int Id { get; set; }
}

public class ProductSubGroupVmHandler : VmQueryHandler, IRequestHandler<ProductSubGroupVmQuery, ProductSubGroupVm>
{
    public ProductSubGroupVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSubGroupVm> Handle(ProductSubGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSubGroups.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductGroup);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductSubGroupId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductSubGroupVm>(entity);
    }
}