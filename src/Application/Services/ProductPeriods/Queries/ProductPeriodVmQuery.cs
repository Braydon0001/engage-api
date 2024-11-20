// auto-generated
namespace Engage.Application.Services.ProductPeriods.Queries;

public class ProductPeriodVmQuery : IRequest<ProductPeriodVm>
{
    public int Id { get; set; }
}

public class ProductPeriodVmHandler : VmQueryHandler, IRequestHandler<ProductPeriodVmQuery, ProductPeriodVm>
{
    public ProductPeriodVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductPeriodVm> Handle(ProductPeriodVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductPeriods.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductYear);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductPeriodId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductPeriodVm>(entity);
    }
}