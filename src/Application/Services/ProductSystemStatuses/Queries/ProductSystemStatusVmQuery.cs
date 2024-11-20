// auto-generated
namespace Engage.Application.Services.ProductSystemStatuses.Queries;

public class ProductSystemStatusVmQuery : IRequest<ProductSystemStatusVm>
{
    public int Id { get; set; }
}

public class ProductSystemStatusVmHandler : VmQueryHandler, IRequestHandler<ProductSystemStatusVmQuery, ProductSystemStatusVm>
{
    public ProductSystemStatusVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSystemStatusVm> Handle(ProductSystemStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSystemStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductSystemStatusId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductSystemStatusVm>(entity);
    }
}