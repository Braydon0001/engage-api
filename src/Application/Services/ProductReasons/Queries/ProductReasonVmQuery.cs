// auto-generated
namespace Engage.Application.Services.ProductReasons.Queries;

public class ProductReasonVmQuery : IRequest<ProductReasonVm>
{
    public int Id { get; set; }
}

public class ProductReasonVmHandler : VmQueryHandler, IRequestHandler<ProductReasonVmQuery, ProductReasonVm>
{
    public ProductReasonVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductReasonVm> Handle(ProductReasonVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductReasons.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductReasonId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductReasonVm>(entity);
    }
}